// <copyright file="Profiler.cs" company="Automate The Planet Ltd.">
// Copyright 2016 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>http://automatetheplanet.com/</site>
using System;
using System.Diagnostics;
using System.Text;

namespace WhichWorksFasterNullDefault
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
            for (var i = 0; i < iterations; i++)
            {
                actionToProfile();
            }
            watch.Stop();
          
            return watch.Elapsed;
        }

        public static string FormatProfileResults(long iterations, TimeSpan profileResults)
        {
            var sb = new StringBuilder();
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
