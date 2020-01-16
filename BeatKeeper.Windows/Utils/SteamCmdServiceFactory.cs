using BeatKeeper.Kernel.Services;

namespace BeatKeeper.Windows.Utils
{
    public class SteamCmdServiceFactory : ISteamCmdServiceFactory
    {
        public static SteamCmdServiceFactory Instance { get; } = new SteamCmdServiceFactory();

        public SteamCmdService Build()
        {
            return new SteamCmdService(ClientPathUtils.SteamCmdFolder);
        }
    }
}