using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatSaberKeeper.App.Utils
{
    public static class ThreadUtils
    {
        public static readonly Action NoAction = () => { };
        
        public static void WaitUntil(
            Func<bool> eval,
            Action callback,
            int checkInterval = 1000)
        {
            RunInBackgroundThread(() =>
            {
                while (!eval())
                {
                    Thread.Sleep(checkInterval);
                }
                callback();
            });
        }

        public static void RunInBackgroundThread(Action action)
        {
            ThreadPool.QueueUserWorkItem(delegate {
                action();
            }, null);
        }

        public static void RunInBackgroundThread(
            this Control ctrl,
            Action action,
            Action callback = null)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                action();
                RunInUiThread(ctrl, callback ?? NoAction);
            }, null);
        }

        public static void RunInBackgroundThread(
            this Control ctrl,
            Func<Task> action,
            Action callback = null)
        {
            ThreadPool.QueueUserWorkItem(async delegate
            {
                await action();
                RunInUiThread(ctrl, callback ?? NoAction);
            }, null);
        }

        public static void RunInUiThread(
            this Control ctrl,
            Action action)
        {
            ctrl.BeginInvoke(new MethodInvoker(action));
        }
    }
}
