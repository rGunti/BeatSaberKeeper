using System;
using System.Threading;
using System.Windows.Forms;

namespace BeatKeeper.Utils
{
    public static class Threading
    {
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
            Action callback)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                action();
                RunInUiThread(ctrl, callback);
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
