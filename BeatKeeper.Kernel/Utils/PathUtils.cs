using System.IO;
using System.Linq;
using System.Reflection;

namespace BeatKeeper.Kernel.Utils
{
    public static class PathUtils
    {
        public static readonly string BaseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static readonly string ResourceDirectory = Path.Combine(BaseDirectory, "resources");

        public static string GetResourcePath(params string[] elements)
        {
            return Path.Combine(
                new string[] { ResourceDirectory }
                    .Concat(elements)
                    .ToArray());
        }

        public static string ReplaceIllegalChars(
            this string path,
            char replaceWith = '-')
        {
            foreach (var invalidFileNameChar in Path.GetInvalidPathChars())
            {
                path = path.Replace(invalidFileNameChar, replaceWith);
            }

            return path;
        }
    }
}
