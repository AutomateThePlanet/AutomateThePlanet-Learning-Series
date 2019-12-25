using System.IO;
using System.Reflection;

namespace BenchmarkingAutomatedTesting
{
    public static class DriverExecutablePathResolver
    {
        public static string GetDriverExecutablePath()
        {
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var di = new DirectoryInfo(assemblyFolder);
            for (int i = 0; i < 4; i++)
            {
                di = di.Parent;
            }

            return di.FullName;
        }
    }
}
