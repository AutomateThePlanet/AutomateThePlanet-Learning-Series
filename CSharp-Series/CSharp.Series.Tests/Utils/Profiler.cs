using System;
using System.Diagnostics;
using System.Text;

namespace CSharp.Series.Tests.Utils
{
    public static class Profiler
    {
        public static TimeSpan Profile(long iterations, Action actionToProfile)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            var watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < iterations; i++)
            {
                actionToProfile();
            }
            watch.Stop();
          
            return watch.Elapsed;
        }

        public static string FormatProfileResults(long iterations, TimeSpan profileResults)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("Total: {0:0.00} ms ({1:N0} ticks) (over {2:N0} iterations)",
              profileResults.TotalMilliseconds, profileResults.Ticks, iterations));
            var avgElapsedMillisecondsPerRun = profileResults.TotalMilliseconds / (double)iterations;
            var avgElapsedTicksPerRun = profileResults.Ticks / (double)iterations;
            sb.AppendLine(string.Format("AVG: {0:0.00} ms ({1:N0} ticks) (over {2:N0} iterations)",
                avgElapsedMillisecondsPerRun, avgElapsedTicksPerRun, iterations));

            return sb.ToString();
        }
    }
}
