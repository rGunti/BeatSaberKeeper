using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeatKeeper.App.Core.Utils
{
    public static class ThreadUtils
    {
        public static async Task RunParallel(
            this IEnumerable<Func<Task>> taskFactories,
            int maxParallelProcesses = 3)
        {
            var queue = taskFactories.ToArray();
            if (!queue.Any())
            {
                return;
            }

            var inFlightTasks = new List<Task>();
            int taskPointer = 0;

            do
            {
                while (inFlightTasks.Count < maxParallelProcesses && taskPointer < queue.Length)
                {
                    Func<Task> factory = queue[taskPointer++];
                    inFlightTasks.Add(factory());
                }
                Task completedTask = await Task.WhenAny(inFlightTasks).ConfigureAwait(false);
                await completedTask.ConfigureAwait(false);

                inFlightTasks.Remove(completedTask);
            } while (taskPointer < queue.Length || inFlightTasks.Count != 0);
        }
    }
}
