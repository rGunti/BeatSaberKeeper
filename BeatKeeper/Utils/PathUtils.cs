using System.IO;
using System.Linq;
using System.Reflection;

namespace BeatKeeper.Utils
{
    public static class PathUtils
    {
        public static readonly string BaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public static string GetResourcePath(params string[] elements)
        {
            return Path.Combine(
                new string[] { BaseDirectory }
                    .Concat(elements)
                    .ToArray());
        }
    }
}
