namespace BeatKeeper.App.Core.Steam
{
    public enum SteamLoginResult
    {
        Unknown,
        Success,
        Failed,
        RequiresSteamGuard,
        Requires2FA,
        SavedLoginNotExistant,
        SavedLoginInvalid
    }
}
