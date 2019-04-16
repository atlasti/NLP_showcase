using System.IO;
using System.Runtime.CompilerServices;

namespace Tests
{
    public static class ProjectExtensions
    {
        private static string RetrieveTestResourcesPath([CallerFilePath] string sourceFilePath = "") =>
            Path.GetFullPath($"{Path.GetDirectoryName(sourceFilePath)}/TestData");

        public static string TestFilePath(this string testFileName) => $"{RetrieveTestResourcesPath()}/{testFileName}";
    }
}
