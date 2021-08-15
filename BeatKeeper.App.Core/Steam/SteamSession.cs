using BeatKeeper.App.Core.Steam.Manifest;
using BeatKeeper.App.Core.Utils;
using Serilog;
using SteamKit2;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace BeatKeeper.App.Core.Steam
{
    public delegate void SteamSessionEventHandler<TEvent>(SteamSession session, TEvent e);

    public class SteamSession : IDisposable
    {
        private static readonly SteamSession instance = new SteamSession();
        public static SteamSession Instance => instance;

        public event SteamSessionEventHandler<SteamClient.DisconnectedCallback> OnClientDisconnected;

        private readonly ILogger _logger;
        private readonly string _workingDirectory;

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
        private Dictionary<uint, ulong> _appTokens = new Dictionary<uint, ulong>();
        private Dictionary<uint, SteamApps.PICSProductInfoCallback.PICSProductInfo> _appInfo
            = new Dictionary<uint, SteamApps.PICSProductInfoCallback.PICSProductInfo>();
        private ConcurrentDictionary<string, SteamApps.CDNAuthTokenCallback> _cdnAuthTokens
            = new ConcurrentDictionary<string, SteamApps.CDNAuthTokenCallback>();

        private object _sentryLock = new object();
        private byte[] _sentryFile = Array.Empty<byte>();

        protected SteamSession(string workingDirectory = null)
        {
            _logger = Log.ForContext<SteamSession>();
            _workingDirectory = PathUtils.EnsureDirectory(workingDirectory ?? BSKConstants.Paths.DefaultWorkingPath);

            _steamClient = new SteamClient();
            _callbackManager = new CallbackManager(_steamClient);
            _steamUser = _steamClient.GetHandler<SteamUser>();
            _steamApps = _steamClient.GetHandler<SteamApps>();

            _onClientConnectedSub = SubscribeToEvent<SteamClient.ConnectedCallback>(
                null,
                _ => IsConnected = true);
            _onClientDisconnectedSub = SubscribeToEvent(
                OnClientDisconnected,
                e => ClientDisconnected(e));

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

        public SteamUser.LoggedOnCallback LastLogonInfo { get; private set; }

        public SteamClient Client => _steamClient;

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
                    _callbackManager.RunWaitCallbacks();
                }
                sub.Dispose();

                if (!didCallbackRun)
                {
                    throw new TimeoutException($"Did not receive callback for {typeof(T)} within {timeout}");
                }

                return returnValue;
            });
        }

        private Task<List<T>> CallbackResultUntil<T>(
            AsyncJobMultiple<T> job,
            Func<T, bool> condition,
            TimeSpan timeout,
            bool returnOnTimeout = false) where T : CallbackMsg
        {
            return Task.Run(() =>
            {
                bool didFinish = false;
                var returnValue = new List<T>();
                var started = DateTime.UtcNow;

                var sub = _callbackManager.Subscribe<T>(job, t =>
                {
                    returnValue.Add(t);
                    didFinish = condition(t);
                });

                while (!didFinish && (DateTime.UtcNow - started) < timeout)
                {
                    _callbackManager.RunWaitCallbacks(TimeSpan.FromSeconds(5));
                }
                sub.Dispose();

                if (!didFinish && !returnOnTimeout)
                {
                    throw new TimeoutException($"Did not receive callback for {typeof(T)} within {timeout}");
                }

                return returnValue;
            });
        }

        private void UpdateMachineAuth(SteamSession session, SteamUser.UpdateMachineAuthCallback e)
        {
            _logger.Debug("Update machine auth request received");

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
            _logger.Debug("Client disconnected");
            IsConnected = false;
            IsLoggedIn = false;
            LoggedInUser = null;
            LastLogonInfo = null;
        }

        private void LicenseListReceived(SteamSession session, SteamApps.LicenseListCallback e)
        {
            _logger.Debug("Received License List");
            if (e.Result == EResult.OK)
            {
                _userLicenses = e.LicenseList;
            }
        }

        private void LoginKeyReceived(SteamSession session, SteamUser.LoginKeyCallback e)
        {
            _logger.Debug("Received Login Key");
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
                }
                catch (Exception)
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
            _logger.Information("Connecting to Steam ...");
            return await CallbackResult<SteamClient.ConnectedCallback>(
                () => _steamClient.Connect());
        }

        public async Task<SteamClient.DisconnectedCallback> Disconnect()
        {
            _logger.Information("Disconnecting Steam session ...");
            return await CallbackResult<SteamClient.DisconnectedCallback>(
                () => _steamClient.Disconnect());
        }

        public async Task<SteamLoginResult> Login(
            string username, string password,
            string authCode = null, string twoFACode = null,
            bool rememeberPassword = false,
            bool tryLoadingFromSaved = false)
        {
            _logger.Debug($"Starting login process ...");

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
                _logger.Error($"Connection failed, could not connect to Steam!");
                throw new Exception("Cannot login because client is not connected to Steam.");
            }

            byte[] sentryHash = null;
            if (_sentryFile.Length > 0)
            {
                lock (_sentryLock)
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

            _logger.Debug("Logging on as {username} ...", username);
            var logon = await CallbackResult<SteamUser.LoggedOnCallback>(
                () => _steamUser.LogOn(logonDetails));

            _logger.Debug("Logon resulted: {result} / {extendedResult}", logon.Result, logon.ExtendedResult);

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
                LastLogonInfo = logon;
                return SteamLoginResult.Success;
            }
        }

        public async Task<SteamApps.PICSTokensCallback> GetAppTokens(uint appId)
        {
            _logger.Debug("Checking tokens for app {appId} ...", appId);
            if (!IsConnected)
            {
                throw new Exception($"Not connected to Steam");
            }

            var tokens = await CallbackResult(
                _steamApps.PICSGetAccessTokens(new[] { appId }, Array.Empty<uint>()),
                TimeSpan.FromSeconds(10));

            foreach (var token in tokens.AppTokens)
            {
                _appTokens[token.Key] = token.Value;
            }

            return tokens;
        }

        public async Task<bool> CheckAccountAccessForApp(uint appId)
        {
            _logger.Debug("Checking licenses for app {appId} ...", appId);
            var tokens = await GetAppTokens(appId);
            return !tokens.AppTokensDenied.Contains(appId);
        }

        public async Task<SteamApps.CDNAuthTokenCallback> RequestCDNAuthToken(uint appId, uint depotId, string host)
        {
            _logger.Debug("Requesting CDN auth token for app {appId}, depot {depotId}, host {host}", appId, depotId, host);
            var key = $"{depotId:D}:{host}";

            if (_cdnAuthTokens.ContainsKey(key))
                return _cdnAuthTokens.GetValueOrDefault(key);

            var authResult = await CallbackResult(
                _steamApps.GetCDNAuthToken(appId, depotId, host),
                TimeSpan.FromSeconds(10));
            if (authResult.Result == EResult.OK)
            {
                _cdnAuthTokens.AddOrUpdate(key, _ => authResult, (_, _) => authResult);
            }
            return (authResult.Result == EResult.OK) ? authResult : null;
        }

        public async void GetManifestForApp(uint appId, uint depotId, string branch = "Public")
        {
            if (depotId == uint.MaxValue)
            {
                _logger.Warning("No valid depot ID provided, currently not supported");
                throw new NotImplementedException($"{nameof(GetManifestForApp)} does not currently support operation without specifying a valid depot ID");
            }

            var depots = await GetAppSection(appId, EAppInfoSection.Depots);
            var depotIds = new List<uint>();
            if (depots != null)
            {
                foreach (var depotSection in depots.Children)
                {
                    uint id = uint.MaxValue;
                    if (!depotSection.Children.Any())
                    {
                        _logger.Debug("No children found for Depot section {depotSection}", depotSection.Name);
                        continue;
                    }

                    if (!uint.TryParse(depotSection.Name, out id))
                    {
                        _logger.Information("Depot section {depotSection} contains an invalid ID (not uint)", depotSection.Name);
                        continue;
                    }

                    if (depotId != uint.MaxValue && id != depotId)
                    {
                        _logger.Information("Depot ID of section does not match provided depot ID; expected={expected} got={got}",
                            depotId, id);
                        continue;
                    }

                    // TODO: Provide support for missing depot ID

                    depotIds.Add(depotId);
                }
            }

            if (!depotIds?.Any() ?? true)
            {
                _logger.Error("Could not find any depots to download for app {appId} or depot {depotId} is not listed!", appId, depotId);
            }

            var infos = new List<DepotDownloadInfo>();
            foreach (var depot in depotIds)
            {
                var info = await GetDepotDownloadInfo(appId, depot, ulong.MaxValue, branch);
                if (info != null)
                {
                    infos.Add(info);
                }
            }
        }

        public async Task<DepotFileData> GetFileListForDepotAndManifest(uint appId, DepotDownloadInfo depot, CancellationTokenSource cts)
        {
            var pool = new CDNClientPool(this, appId);
            var counter = new DepotDownloadCounter();

            ProtoManifest oldProtoManifest = null;
            ProtoManifest newProtoManifest = null;

            var tempFolder = PathUtils.EnsureDirectory(BSKConstants.Paths.Temp);
            var manifestFileName = Path.Combine(tempFolder, $"{depot.Id}_{depot.ManifestId}.bin");
            var manifestShaFileName = manifestFileName.AppendExtension(BSKConstants.FileExtensions.CHECKSUM);

            byte[] expectedChecksum;
            try
            {
                _logger.Debug("Reading checksum for {manifestFile}", manifestFileName);
                expectedChecksum = File.ReadAllBytes(manifestShaFileName);
            }
            catch (IOException)
            {
                _logger.Debug("Could not read checksum for {manifestFile}", manifestFileName);
                expectedChecksum = null;
            }

            newProtoManifest = ProtoManifest.LoadFromFile(manifestFileName, out var currentChecksum);
            if (newProtoManifest != null && (expectedChecksum == null || !expectedChecksum.SequenceEqual(currentChecksum)))
            {
                _logger.Error("Manifest {depotId},{manifestId} on disk did not match expected checksum", depot.Id, depot.ManifestId);
                newProtoManifest = null;
            }

            if (newProtoManifest != null)
            {
                _logger.Information("Already have manifest {depotId},{manifestId}",
                    depot.Id, depot.ManifestId);
            }
            else
            {
                _logger.Debug("Downloading depot manifest {depotId},{manifestId} ...", depot.Id, depot.ManifestId);
                DepotManifest depotManifest = null;

                do
                {
                    try
                    {
                        CDNClient.Server connection = pool.GetConnection(cts.Token);
                        var cdnToken = await pool.AuthenticateConnection(appId, depot.Id, connection);
                        depotManifest = await pool.CDNClient.DownloadManifestAsync(depot.Id, depot.ManifestId,
                            connection, cdnToken, depot.DepotKey);

                        pool.ReturnConnection(connection);
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                    catch (SteamKitWebRequestException ex)
                    {
                        _logger.Error("Encountered HTTP error {ex} while downloading {depotId},{manifestId}", ex.StatusCode, depot.Id, depot.ManifestId);
                        if (ex.StatusCode == HttpStatusCode.Unauthorized || ex.StatusCode == HttpStatusCode.Forbidden || ex.StatusCode == HttpStatusCode.NotFound)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Fatal(ex,
                            "Encountered error while downloading manifest {depotId},{manifestId}",
                            depot.Id, depot.ManifestId);
                    }
                } while (depotManifest == null);

                if (depotManifest == null)
                {
                    _logger.Error("Unable to download manifest {depotId},{manifestId}", depot.Id, depot.ManifestId);
                    cts.Cancel();
                }

                cts.Token.ThrowIfCancellationRequested();

                byte[] checksum;
                newProtoManifest = new ProtoManifest(depotManifest, depot.ManifestId);
                newProtoManifest.SaveToFile(manifestFileName, out checksum);
                File.WriteAllBytes(manifestShaFileName, checksum);

                _logger.Debug("Downloaded manifest {depotId},{manifestId}", depot.Id, depot.ManifestId);
            }

            newProtoManifest.Files.Sort((x, y) => string.Compare(x.FileName, y.FileName, StringComparison.Ordinal));
            _logger.Debug("Manifest {depotId},{manifestId} received, created {creationTime}", depot.Id, depot.ManifestId, newProtoManifest.CreationTime);

#if DEBUG
            var manifestDump = Path.Combine(tempFolder, $"manifest_{depot.Id}_{depot.ManifestId}.json");
            ManifestSaver.WriteManifestToFile(newProtoManifest, manifestDump);
#endif

            var filesToDownload = newProtoManifest.Files;
            var allFileNames = new HashSet<string>(filesToDownload.Count);
            _logger.Debug("{count} files to download from {depotId},{manifestId}", allFileNames.Count, depot.Id, depot.ManifestId);

            var baseStagingPath = PathUtils.ConstructStagingFilePath(appId, depot.Id, depot.ManifestId).EnsureDirectory();

            foreach (var file in filesToDownload)
            {
                allFileNames.Add(file.FileName);

                var stagingFilePath = Path.Combine(baseStagingPath, file.FileName);
                if (file.Flags.HasFlag(EDepotFileFlag.Directory))
                {
                    // TODO: Let's do this in the downloader instead
                    //stagingFilePath.EnsureDirectory();
                }
                else
                {
                    // TODO: Let's do this in the downloader instead
                    //Path.GetDirectoryName(stagingFilePath).EnsureDirectory();
                    counter.CompleteDownloadSize += file.TotalSize;
                }
            }

            return new DepotFileData
            {
                AllFiles = filesToDownload,
                AllFileNames = allFileNames,
                DepotDownloadInfo = depot,
                DepotCounter = counter,
                StagingDir = baseStagingPath,
                Manifest = newProtoManifest,
                PreviousManifest = oldProtoManifest
            };
        }

        public async Task DownloadDepot(uint appId, DepotFileData depotFileData, Action<string> actionReport, CancellationTokenSource cts)
        {
            var depot = depotFileData.DepotDownloadInfo;
            var counter = depotFileData.DepotCounter;

            var files = depotFileData.AllFiles
                .Where(f => !f.Flags.HasFlag(EDepotFileFlag.Directory))
                .ToArray();
            var networkChunkQueue = new ConcurrentQueue<Tuple<FileStreamData, ProtoManifest.FileData, ProtoManifest.ChunkData>>();

            await files.Select(f => new Func<Task>(async () =>
                await Task.Run(() => DownloadFileInfoAsync(depotFileData, f, networkChunkQueue, actionReport, cts))))
                .RunParallel();

            await networkChunkQueue.Select(x => new Func<Task>(async () =>
                await Task.Run(() => DownloadFileChunkAsync(appId, counter, depotFileData, x, actionReport, cts))))
                .RunParallel();
        }

        private void DownloadFileInfoAsync(
            DepotFileData depotFileData,
            ProtoManifest.FileData file,
            ConcurrentQueue<Tuple<FileStreamData, ProtoManifest.FileData, ProtoManifest.ChunkData>> networkChunkQueue,
            Action<string> actionReport,
            CancellationTokenSource cts)
        {
            cts.Token.ThrowIfCancellationRequested();

            var depot = depotFileData.DepotDownloadInfo;
            var stagingDir = depotFileData.StagingDir;
            var counter = depotFileData.DepotCounter;

            actionReport?.Invoke($"{file.FileName}: Preparing download ...");
            _logger.Verbose("Downloading file info for {fileName} ...", file.FileName);

            string stagingFilePath = Path.Combine(stagingDir, file.FileName).EnsureDirectoryForFile();
            if (File.Exists(stagingFilePath))
            {
                File.Delete(stagingFilePath);
            }

            FileStream fs = null;
            List<ProtoManifest.ChunkData> neededChunks;
            FileInfo fi = new FileInfo(stagingFilePath);
            if (!fi.Exists)
            {
                actionReport?.Invoke($"{file.FileName}: Reserving disk space ...");
                _logger.Verbose("{fileName}: Reserving space for file ...", file.FileName);
                fs = File.Create(stagingFilePath);
                fs.SetLength((long)file.TotalSize);
                neededChunks = new List<ProtoManifest.ChunkData>(file.Chunks);
            }
            else
            {
                actionReport?.Invoke($"{file.FileName}: Validating existing file ...");
                _logger.Verbose("{fileName}: Validating existing file ...", file.FileName);
                fs = File.Open(stagingFilePath, FileMode.Open);
                if ((ulong)fi.Length != file.TotalSize)
                {
                    fs.SetLength((long)file.TotalSize);
                }
                neededChunks = SteamUtils.ValidateSteamFileChecksums(fs, file.Chunks.OrderBy(x => x.Offset).ToArray());
            }

            if (neededChunks.None())
            {
                _logger.Debug("{fileName}: No more chunks needed, disposing ...", file.FileName);
                lock (counter)
                {
                    counter.SizeDownloaded += file.TotalSize;
                }
                if (fs != null)
                    fs.Dispose();
                return;
            }
            else
            {
                var sizeOnDisk = file.TotalSize - (ulong)neededChunks.Select(x => (long)x.UncompressedLength).Sum();
                lock (counter)
                {
                    counter.SizeDownloaded += sizeOnDisk;
                }
            }

            var fsData = new FileStreamData
            {
                Stream = fs,
                Lock = new SemaphoreSlim(1),
                chunksToDownload = neededChunks.Count
            };
            actionReport?.Invoke($"{file.FileName}: Queueing {neededChunks.Count} to download ...");
            _logger.Debug("{fileName}: Queueing {chunkCount} required chunks ...", file.FileName, neededChunks.Count);
            foreach (var chunk in neededChunks)
            {
                networkChunkQueue.Enqueue(Tuple.Create(fsData, file, chunk));
            }
        }

        private async void DownloadFileChunkAsync(
            uint appId,
            DepotDownloadCounter globalCounter,
            DepotFileData depotFileData,
            Tuple<FileStreamData, ProtoManifest.FileData, ProtoManifest.ChunkData> chunkInfo,
            Action<string> actionReport,
            CancellationTokenSource cts)
        {
            cts.Token.ThrowIfCancellationRequested();

            var depot = depotFileData.DepotDownloadInfo;
            var counter = depotFileData.DepotCounter;

            var fsData = chunkInfo.Item1;
            var file = chunkInfo.Item2;
            var chunk = chunkInfo.Item3;
            
            string chunkId = Convert.ToHexString(chunk.ChunkID);
            var data = new DepotManifest.ChunkData {
                ChunkID = chunk.ChunkID,
                Checksum = chunk.Checksum,
                Offset = chunk.Offset,
                CompressedLength = chunk.CompressedLength,
                UncompressedLength = chunk.UncompressedLength
            };

            CDNClient.DepotChunk chunkData = null;
            // TODO: This pool needs to be reusable
            var pool = new CDNClientPool(this, appId);

            _logger.Debug("{fileName}: Downloading file chunk {chunkId} ...", file.FileName, chunkId);

            do
            {
                cts.Token.ThrowIfCancellationRequested();
                CDNClient.Server connection;

                try
                {
                    connection = pool.GetConnection(cts.Token);
                    var token = await pool.AuthenticateConnection(appId, depot.Id, connection);

                    actionReport?.Invoke($"{file.FileName}: Downloading file chunks ({chunkId}) ...");
                    chunkData = await pool.CDNClient.DownloadDepotChunkAsync(depot.Id, data,
                        connection, token, depot.DepotKey).ConfigureAwait(false);

                    pool.ReturnConnection(connection);
                } catch (TaskCanceledException)
                {
                    _logger.Warning("{fileName}: Connection timed out while downloading chunk {chunk}", file.FileName, chunkId);
                } catch (SteamKitWebRequestException e)
                {
                    if (e.StatusCode == HttpStatusCode.Unauthorized || e.StatusCode == HttpStatusCode.Forbidden)
                    {
                        _logger.Warning("{fileName}: Encountered 401 for chunk {chunk}, aborting!", file.FileName, chunkId);
                        break;
                    }
                    else
                    {
                        _logger.Warning("{fileName}: Failed to download chunk {chunk}, got {statusCode}", file.FileName, chunkId, e.StatusCode);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                } catch (Exception e)
                {
                    _logger.Error(e, "{fileName}: Unexpected exception while downloading chunk {chunk}", file.FileName, chunkId);
                }
            } while (chunkData == null);

            if (chunkData == null)
            {
                _logger.Warning("Failed to find any server with chunk {chunkId} for depot {depotId}, aborting ...",
                    chunkId, depot.Id);
                cts.Cancel();
            }
            cts.Token.ThrowIfCancellationRequested();

            try
            {
                actionReport?.Invoke($"{file.FileName}: Writing file to disk ...");
                await fsData.Lock.WaitAsync().ConfigureAwait(false);

                var stream = fsData.Stream;
                stream.Seek((long)chunkData.ChunkInfo.Offset, SeekOrigin.Begin);
                await stream.WriteAsync(chunkData.Data.AsMemory(0, chunkData.Data.Length));
            }
            finally
            {
                fsData.Lock.Release();
            }

            int remainingChunks = Interlocked.Decrement(ref fsData.chunksToDownload);
            if (remainingChunks == 0)
            {
                _logger.Debug("{fileName}: No more chunks needed, disposing File Stream and lock ...", file.FileName);
                fsData.Stream.Dispose();
                fsData.Lock.Dispose();
            }

            ulong sizeDownloaded = 0;
            lock (counter)
            {
                sizeDownloaded = counter.SizeDownloaded + (ulong)chunkData.Data.Length;
                counter.SizeDownloaded = sizeDownloaded;
                counter.DepotBytesCompressed += chunk.CompressedLength;
                counter.DepotBytesUncompressed += chunk.UncompressedLength;

                _logger.Verbose("{fileName}: Downloaded {size} bytes", file.FileName, sizeDownloaded);
            }

            // TODO global counter
            if (remainingChunks == 0)
            {
                // TODO report progress
            }
        }

        public async Task<DepotDownloadInfo> GetDepotDownloadInfo(uint appId, uint depotId, ulong manifestId, string branch)
        {
            string contentName = await GetAppOrDepotName(appId, depotId);
            // TODO: Check license access

            if (manifestId == ulong.MaxValue)
            {
                // TODO: Skipped
            }

            uint version = await GetAppBuildNumber(appId, branch);
            var key = await GetDepotKey(appId, depotId);
            if (key == null)
            {
                _logger.Error("Failed to get depot key for {appId},{depotId}", appId, depotId);
                return null;
            }

            var depotKey = key.Item2;

            return new DepotDownloadInfo(depotId, manifestId, contentName, depotKey);
        }

        private async Task<Tuple<uint, byte[]>> GetDepotKey(uint appId, uint depotId)
        {
            var depotKey = await CallbackResult(
                _steamApps.GetDepotDecryptionKey(depotId, appId),
                TimeSpan.FromSeconds(10));
            return depotKey.Result != EResult.OK ? null : new Tuple<uint, byte[]>(depotKey.DepotID, depotKey.DepotKey);
        }

        private async Task<uint> GetAppBuildNumber(uint appId, string branch)
        {
            if (appId == uint.MaxValue)
                return 0;

            var depots = await GetAppSection(appId, EAppInfoSection.Depots);
            var branches = depots["branches"];
            var node = branches[branch];

            if (node == KeyValue.Invalid)
                return 0;

            var buildId = node["buildid"];
            if (buildId == KeyValue.Invalid)
                return 0;

            return uint.Parse(buildId.Value);
        }

        private async Task<string> GetAppOrDepotName(uint appId, uint depotId)
        {
            if (depotId == uint.MaxValue)
            {
                var info = await GetAppSection(appId, EAppInfoSection.Common);
                if (info == null)
                    return string.Empty;

                return info["name"].AsString();
            }

            var depots = await GetAppSection(appId, EAppInfoSection.Depots);
            if (depots == null)
                return string.Empty;

            var depotChild = depots[$"{depotId}"];
            if (depotChild == null)
                return string.Empty;

            return depotChild["name"].AsString();
        }

        public async Task<List<SteamApps.PICSProductInfoCallback>> GetProductInfo(uint appId)
        {
            _logger.Debug("Getting product info for app {appId} ...", appId);

            var appRequest = new SteamApps.PICSRequest(appId);
            if (_appTokens.ContainsKey(appId))
            {
                appRequest.AccessToken = _appTokens[appId];
                appRequest.Public = false;
            }

            var list = await CallbackResultUntil(
                _steamApps.PICSGetProductInfo(new[] { appRequest }, Array.Empty<SteamApps.PICSRequest>()),
                r => !r.ResponsePending,
                TimeSpan.FromSeconds(60));
            foreach (var response in list)
            {
                foreach (var app in response.Apps)
                {
                    _appInfo[app.Key] = app.Value;
                }
                foreach (var app in response.UnknownApps)
                {
                    _appInfo[app] = null;
                }
            }

            return list;
        }

        public async Task<KeyValue> GetAppSection(uint appId, EAppInfoSection section = EAppInfoSection.Depots)
        {
            _logger.Debug("Trying to get app section {section} for app {appId}", section, appId);

            SteamApps.PICSProductInfoCallback.PICSProductInfo app;
            if (!_appInfo.TryGetValue(appId, out app))
            {
                _logger.Warning("Couldn't find app {appId} in memory, retry fetching ...", appId);
                await GetProductInfo(appId);
                if (!_appInfo.TryGetValue(appId, out app))
                {
                    _logger.Warning("Couldn't find app {appId} online", appId);
                    return null;
                }
            }

            var appInfo = app.KeyValues;
            string sectionKey = $"{section}".ToLower();

            return appInfo.Children
                .FirstOrDefault(c => c.Name == sectionKey);
        }

        public void Dispose()
        {
            _logger.Debug("Disposing Steam Session ...");
            if (IsConnected)
            {
                Disconnect().GetAwaiter().GetResult();
            }

            _onClientConnectedSub?.Dispose();
            _onClientDisconnectedSub?.Dispose();
            _onUpdateMachineAuthSub?.Dispose();
            _onLicenseListReceived?.Dispose();
            _onLoginKeyReceived?.Dispose();

            _logger.Information("Steam Session disposed");
        }
    }
}
