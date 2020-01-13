namespace BeatKeeper.Controls
{
    public interface IStatusDialog
    {
        void SetStatus(string status);
        void SetStatus(string status, int value, int maxValue = 100);
    }
}