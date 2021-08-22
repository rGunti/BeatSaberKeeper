namespace BeatSaberKeeper.Kernel.Services
{
    public interface ISteamCmdServiceFactory
    {
        SteamCmdService Build();
    }

    public class PrebuiltSteamCmdServiceFactory : ISteamCmdServiceFactory
    {
        private readonly string _path;

        public PrebuiltSteamCmdServiceFactory(string path)
        {
            _path = path;
        }

        public SteamCmdService Build()
        {
            return new SteamCmdService(_path);
        }
    }
}