using Serilog;
using SteamKit2;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BeatSaberKeeper.App.Core.Steam
{
    public class CDNClientPool : IDisposable
    {
        private const int ServerEndpointMinimumSize = 8;

        private readonly ILogger _logger;
        private readonly uint _appId;

        private readonly SteamSession _steamSession;
        private readonly ConcurrentStack<CDNClient.Server> _activeConnectionPool;
        private readonly BlockingCollection<CDNClient.Server> _availableServerEndpoints;

        private readonly AutoResetEvent _populatePoolEvent;
        private readonly Task _monitorTask;
        private readonly CancellationTokenSource _shutdownToken;

        public CDNClient CDNClient { get; }
        public CancellationTokenSource ExhaustedToken { get; set; }

        public CDNClientPool(SteamSession session, uint appId)
        {
            _logger = Log.ForContext<CDNClientPool>();
            _logger.Debug("Initializing new CDN Client Pool {appId}", appId);

            _steamSession = session;
            _appId = appId;

            CDNClient = new CDNClient(session.Client);

            _activeConnectionPool = new ConcurrentStack<CDNClient.Server>();
            _availableServerEndpoints = new BlockingCollection<CDNClient.Server>();

            _populatePoolEvent = new AutoResetEvent(true);
            _shutdownToken = new CancellationTokenSource();

            _monitorTask = Task.Factory.StartNew(ConnectionPoolMonitorAsync).Unwrap();
        }

        public void Dispose()
        {
            Shutdown();
        }

        public void Shutdown()
        {
            _logger.Debug("Shutting down CDN Client Pool {appId} ...", _appId);
            _shutdownToken.Cancel();
            _monitorTask.Wait();

            _logger.Information("CDN Client Pool {appId} shut down", _appId);
        }

        private async Task<IReadOnlyCollection<CDNClient.Server>> FetchBootstrapServerListAsync()
        {
            var backoffDelay = 0;

            while (!_shutdownToken.IsCancellationRequested)
            {
                try
                {
                    var cdnServers = await ContentServerDirectoryService.LoadAsync(
                        _steamSession.Client.Configuration,
                        // CellId?
                        _shutdownToken.Token);
                    if (cdnServers != null)
                    {
                        return cdnServers;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to retrieve content server list: {0}", ex.Message);

                    if (ex is SteamKitWebRequestException e && e.StatusCode == (HttpStatusCode)429)
                    {
                        // If we're being throttled, add a delay to the next request
                        backoffDelay = Math.Min(5, ++backoffDelay);
                        await Task.Delay(TimeSpan.FromSeconds(backoffDelay));
                    }
                }
            }

            return null;
        }

        private async Task ConnectionPoolMonitorAsync()
        {
            bool didPopulate = false;

            while (!_shutdownToken.IsCancellationRequested)
            {
                _populatePoolEvent.WaitOne(TimeSpan.FromSeconds(1));

                // We want the Steam session so we can take the CellID from the session and pass it through to the ContentServer Directory Service
                if (_availableServerEndpoints.Count < ServerEndpointMinimumSize && _steamSession.Client.IsConnected)
                {
                    var servers = await FetchBootstrapServerListAsync().ConfigureAwait(false);

                    if (servers == null || servers.Count == 0)
                    {
                        ExhaustedToken?.Cancel();
                        return;
                    }

                    var weightedCdnServers = servers
                        .Where(x => x.Type == "SteamCache" || x.Type == "CDN")
                        .Select(x =>
                        {
                            //AccountSettingsStore.Instance.ContentServerPenalty.TryGetValue(x.Host, out var penalty);

                            return Tuple.Create(x, 1);
                        })
                        .OrderBy(x => x.Item2).ThenBy(x => x.Item1.WeightedLoad);

                    foreach (var (server, weight) in weightedCdnServers)
                    {
                        for (var i = 0; i < server.NumEntries; i++)
                        {
                            _availableServerEndpoints.Add(server);
                        }
                    }

                    didPopulate = true;
                }
                else if (_availableServerEndpoints.Count == 0 && !_steamSession.Client.IsConnected && didPopulate)
                {
                    ExhaustedToken?.Cancel();
                    return;
                }
            }
        }

        private CDNClient.Server BuildConnection(CancellationToken token)
        {
            if (_availableServerEndpoints.Count < ServerEndpointMinimumSize)
            {
                _populatePoolEvent.Set();
            }

            return _availableServerEndpoints.Take(token);
        }

        public CDNClient.Server GetConnection(CancellationToken token)
        {
            if (!_activeConnectionPool.TryPop(out var connection))
            {
                connection = BuildConnection(token);
            }

            return connection;
        }

        private string ResolveCDNTopLevelHost(string host)
        {
            // SteamPipe CDN shares tokens with all hosts
            if (host.EndsWith(".steampipe.steamcontent.com"))
            {
                return "steampipe.steamcontent.com";
            }
            else if (host.EndsWith(".steamcontent.com"))
            {
                return "steamcontent.com";
            }

            return host;
        }

        public async Task<string> AuthenticateConnection(uint appId, uint depotId, CDNClient.Server server)
        {
            var host = ResolveCDNTopLevelHost(server.Host);

            var key = await _steamSession.RequestCDNAuthToken(appId, depotId, host);
            if (key == null)
            {
                throw new Exception($"Failed to retrieve CDN token for server {server.Host} depot {depotId}");
            }
            return key.Token;
        }

        public void ReturnConnection(CDNClient.Server server)
        {
            if (server == null) return;

            _activeConnectionPool.Push(server);
        }
    }
}
