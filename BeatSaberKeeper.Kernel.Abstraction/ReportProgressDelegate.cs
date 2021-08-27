namespace BeatSaberKeeper.Kernel.Abstraction
{
    public delegate void ReportProgressDelegate(string status, int value, int max);

    public static class ReportProgressDelegateExtensions
    {
        public static void Submit(
            this ReportProgressDelegate report,
            string status,
            int value = -1,
            int max = 100)
        {
            report?.Invoke(status, value, max);
        }
    }
}