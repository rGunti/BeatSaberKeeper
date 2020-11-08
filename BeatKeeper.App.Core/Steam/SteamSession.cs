using SteamKit2;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BeatKeeper.App.Core.Steam
{
    public delegate void SteamSessionEventHandler<TEvent>(SteamSession session, TEvent e);

    public class SteamSession : IDisposable
    {
        private static SteamSession instance = new SteamSession();
        public static SteamSession Instance => instance;

        public event SteamSessionEventHandler<SteamClient.DisconnectedCallback> OnClientDisconnected;

        private readonly SteamClient _steamClient;
        private readonly CallbackManager _callbackManager;
        private readonly SteamUser _steamUser;
        private readonly SteamApps _steamApps;

        private readonly IDisposable _onClientConnectedSub;
        private readonly IDisposable _onClientDisconnectedSub;
        private readonly IDisposable _onUpdateMachineAuthSub;
        private readonly IDisposable _onLicenseListReceived;
        private readonly IDisposable _onLoginKeyReceived;

        private IReadOnlyCollection<SteamApps.LicenseListCallback.License> _userLicenses;

        private object _sentryLock = new object();
        private byte[] _sentryFile = new byte[0];

        protected SteamSession()
        {
            _steamClient = new SteamClient();
            _callbackManager = new CallbackManager(_steamClient);
            _steamUser = _steamClient.GetHandler<SteamUser>();
            _steamApps = _steamClient.GetHandler<SteamApps>();

            _onClientConnectedSub = SubscribeToEvent<SteamClient.ConnectedCallback>(
                null,
                _ => IsConnected = true);
            _onClientDisconnectedSub = SubscribeToEvent(
                OnClientDisconnected,
                e => ClientDisconnected(e),
                _ => Debug.WriteLine("*** STEAM CLIENT DISCONNECTED ***"));

            _onUpdateMachineAuthSub = SubscribeToEvent<SteamUser.UpdateMachineAuthCallback>(
                UpdateMachineAuth);
            _onLicenseListReceived = SubscribeToEvent<SteamApps.LicenseListCallback>(
                LicenseListReceived);
            _onLoginKeyReceived = SubscribeToEvent<SteamUser.LoginKeyCallback>(
                LoginKeyReceived);
        }

        public bool IsConnected { get; private set; }
        public bool IsLoggedIn { get; private set; }
        public string LoggedInUser { get; private set; }
        public SteamID LoggedInAccount { get; private set; }

        private IDisposable SubscribeToEvent<T>(
            SteamSessionEventHandler<T> eventHandler,
            params Action<T>[] runBeforeFiringEvent) where T : CallbackMsg
        {
            return _callbackManager.Subscribe<T>(e =>
            {
                if (runBeforeFiringEvent?.Any() ?? false)
                {
                    foreach (var action in runBeforeFiringEvent)
                    {
                        action?.Invoke(e);
                    }
                }
                eventHandler?.Invoke(this, e);
            });
        }

        private async Task<T> CallbackResult<T>(params Action<T>[] doBeforeReturn) where T : CallbackMsg
            => await CallbackResult(() => { }, TimeSpan.FromSeconds(10), doBeforeReturn);

        private async Task<T> CallbackResult<T>(
            Action runBefore,
            params Action<T>[] doBeforeReturn) where T : CallbackMsg
            => await CallbackResult(runBefore, TimeSpan.FromSeconds(10), doBeforeReturn);

        private Task<T> CallbackResult<T>(
            Action runBefore,
            TimeSpan timeout,
            params Action<T>[] doBeforeReturn) where T : CallbackMsg
        {
            return Task.Run(() =>
            {
                bool didCallbackRun = false;
                T returnValue = default;
                var started = DateTime.UtcNow;

                var sub = _callbackManager.Subscribe<T>(t =>
                {
                    returnValue = t;
                    didCallbackRun = true;

                    if (doBeforeReturn?.Any() ?? false)
                    {
                        foreach (var action in doBeforeReturn)
                            action?.Invoke(t);
                    }
                });

                runBefore();
                while (!didCallbackRun && (DateTime.UtcNow - started) < timeout)
                {
                    _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
                }
                sub.Dispose();

                if (!didCallbackRun)
                {
                    throw new TimeoutException($"Did not receive callback for {typeof(T)} within {timeout}");
                }

                return returnValue;
            });
        }

        private Task<T> CallbackResult<T>(
            AsyncJob<T> job,
            TimeSpan timeout,
            params Action<T>[] doBeforeReturn) where T : CallbackMsg
        {
            return Task.Run(() =>
            {
                bool didCallbackRun = false;
                T returnValue = default;
                var started = DateTime.UtcNow;

                var sub = _callbackManager.Subscribe<T>(job, t =>
                {
                    returnValue = t;
                    didCallbackRun = true;

                    if (doBeforeReturn?.Any() ?? false)
                    {
                        foreach (var action in doBeforeReturn)
                            action?.Invoke(t);
                    }
                });

                while (!didCallbackRun && (DateTime.UtcNow - started) < timeout)
                {
                    _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
                }
                sub.Dispose();

                if (!didCallbackRun)
                {
                    throw new TimeoutException($"Did not receive callback for {typeof(T)} within {timeout}");
                }

                return returnValue;
            });
        }

        private void UpdateMachineAuth(SteamSession session, SteamUser.UpdateMachineAuthCallback e)
        {
            Debug.WriteLine("*** STEAM REQUESTED UPDATE MACHINE AUTH ***");

            int size;
            byte[] sentryHash;

            lock (_sentryLock)
            {
                using (var s = new MemoryStream())
                {
                    if (_sentryFile.Length > 0)
                    {
                        s.Write(_sentryFile, 0, _sentryFile.Length);
                    }

                    s.Seek(e.Offset, SeekOrigin.Begin);
                    s.Write(e.Data, 0, e.BytesToWrite);
                    size = (int)s.Length;

                    s.Seek(0, SeekOrigin.Begin);
                    using (var sha = SHA1.Create())
                    {
                        sentryHash = sha.ComputeHash(s);
                    }

                    s.Seek(0, SeekOrigin.Begin);
                    _sentryFile = s.ToArray();
                }
            }

            _steamUser.SendMachineAuthResponse(new SteamUser.MachineAuthDetails
            {
                JobID = e.JobID,
                FileName = e.FileName,
                BytesWritten = e.BytesToWrite,
                FileSize = size,
                Offset = e.Offset,
                Result = EResult.OK,
                LastError = 0,
                OneTimePassword = e.OneTimePassword,
                SentryFileHash = _sentryFile
            });
        }

        private void ClientDisconnected(SteamClient.DisconnectedCallback e)
        {
            IsConnected = false;
            IsLoggedIn = false;
            LoggedInUser = null;
        }

        private void LicenseListReceived(SteamSession session, SteamApps.LicenseListCallback e)
        {
            if (e.Result == EResult.OK)
            {
                _userLicenses = e.LicenseList;
            }
        }

        private void LoginKeyReceived(SteamSession session, SteamUser.LoginKeyCallback e)
        {
            SaveLoginKey(LoggedInUser, e.LoginKey);
            _steamUser.AcceptNewLoginKey(e);
        }

        internal void SaveLoginKey(string username, string key)
        {
            // TODO: replace
            File.WriteAllText("login.sav", $"{username}\n{key}");
        }
        internal Tuple<string, string> LoadLoginKey()
        {
            // TODO: replace
            if (File.Exists("login.sav"))
            {
                var fileContent = File.ReadAllText("login.sav").Split('\n');
                try
                {
                    return new Tuple<string, string>(fileContent[0], fileContent[1]);
                } catch (Exception)
                {
                    File.Delete("login.sav");
                }
            }
            return null;
        }
        public string GetSavedLoginName() => LoadLoginKey()?.Item1;
        public bool HasSavedLogin => File.Exists("login.sav");

        public async Task<SteamClient.ConnectedCallback> Connect()
        {
            return await CallbackResult<SteamClient.ConnectedCallback>(
                () => _steamClient.Connect());
        }

        public async Task<SteamClient.DisconnectedCallback> Disconnect()
        {
            return await CallbackResult<SteamClient.DisconnectedCallback>(
                () => _steamClient.Disconnect());
        }

        public async Task<SteamLoginResult> Login(
            string username, string password,
            string authCode = null, string twoFACode = null,
            bool rememeberPassword = false,
            bool tryLoadingFromSaved = false)
        {
            string loginKey = null;
            if (tryLoadingFromSaved)
            {
                var loginInfo = LoadLoginKey();
                if (loginInfo == null)
                {
                    return SteamLoginResult.SavedLoginNotExistant;
                }
                username = loginInfo.Item1;
                loginKey = loginInfo.Item2;
                authCode = null;
                twoFACode = null;
            }

            if (IsConnected)
            {
                await Disconnect();
                await Task.Delay(2500);
            }
            if (!IsConnected)
            {
                await Connect();
            }
            if (!IsConnected)
            {
                throw new Exception("Cannot login because client is not connected to Steam.");
            }

            byte[] sentryHash = null;
            if (_sentryFile.Length > 0)
            {
                lock(_sentryLock)
                {
                    sentryHash = CryptoHelper.SHAHash(_sentryFile);
                }
            }

            var logonDetails = new SteamUser.LogOnDetails()
            {
                Username = username,
                Password = tryLoadingFromSaved ? null : password,
                ShouldRememberPassword = rememeberPassword,
                LoginID = 0x534b32,
                LoginKey = tryLoadingFromSaved ? loginKey : null,

                AuthCode = authCode,
                TwoFactorCode = twoFACode,

                SentryFileHash = sentryHash
            };

            var logon = await CallbackResult<SteamUser.LoggedOnCallback>(
                () => _steamUser.LogOn(logonDetails));

            bool isSteamGuard = logon.Result == EResult.AccountLogonDenied;
            bool is2FA = logon.Result == EResult.AccountLoginDeniedNeedTwoFactor;

            if (isSteamGuard || is2FA)
            {
                if (is2FA)
                {
                    return SteamLoginResult.Requires2FA;
                }
                else
                {
                    return SteamLoginResult.RequiresSteamGuard;
                }
            }
            else if (logon.Result != EResult.OK)
            {
                return SteamLoginResult.Failed;
            }
            else
            {
                IsLoggedIn = true;
                LoggedInUser = username;
                LoggedInAccount = logon.ClientSteamID;
                return SteamLoginResult.Success;
            }
        }

        public async Task<bool> CheckAccountAccessForApp(uint appId)
        {
            if (!IsConnected)
            {
                throw new Exception($"Not connected to Steam");
            }

            var tokens = await CallbackResult(
                _steamApps.PICSGetAccessTokens(new[] { appId }, Array.Empty<uint>()),
                TimeSpan.FromSeconds(10));
            return !tokens.AppTokensDenied.Contains(appId);
            /* if (tokens.AppTokensDenied.Contains(appId))
            {
                return false;
            }

            var appTokens = new Dictionary<uint, ulong>();
            foreach (var token in tokens.AppTokens)
            {
                appTokens[token.Key] = token.Value;
            }

            var picsRequest = */
        }

        public void Dispose()
        {
            if (IsConnected)
            {
                Disconnect().GetAwaiter().GetResult();
            }

            _onClientConnectedSub?.Dispose();
            _onClientDisconnectedSub?.Dispose();
            _onUpdateMachineAuthSub?.Dispose();
            _onLicenseListReceived?.Dispose();
        }
    }
}
