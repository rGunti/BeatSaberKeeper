using System.Collections.Generic;
using System.Linq;

namespace BeatSaberKeeper.App.Core.Utils
{
    public static class LinqUtils
    {
        public static bool None<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }
    }
}
