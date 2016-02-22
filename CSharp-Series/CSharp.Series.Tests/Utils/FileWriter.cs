using System;
using System.IO;

namespace CSharp.Series.Tests.Utils
{
    public static class FileWriter
    {
        public static void WriteToDesktop(string fileName, string content, bool appendToFile = true)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string resultsPath = Path.Combine(desktopPath, string.Concat(fileName, ".txt"));
            using (StreamWriter writer = new StreamWriter(resultsPath, appendToFile))
            {
                writer.WriteLine(content);
            }
        }
    }
}
