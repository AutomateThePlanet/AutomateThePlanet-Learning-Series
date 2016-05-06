// <copyright file="ReducedAutoMapperTest.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ReducedAutoMapper
{
    [TestClass]
    public class ReducedAutoMapperTest
    {
        [TestMethod]
        public void ProfileTest()
        {
            Profile("Test Reduced AutoMapper 10 Runs 10k Objects", 10, () => MapObjectsReduceAutoMapper());
        }

        static void Profile(string description, int iterations, Action actionToProfile)
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
            System.Console.WriteLine(description);
            System.Console.WriteLine("Total: {0:0.00} ms ({1:N0} ticks) (over {2:N0} iterations)",
                watch.ElapsedMilliseconds, watch.ElapsedTicks, iterations);
            var avgElapsedMillisecondsPerRun = watch.ElapsedMilliseconds / iterations;
            var avgElapsedTicksPerRun = watch.ElapsedMilliseconds / iterations;
            System.Console.WriteLine("AVG: {0:0.00} ms ({1:N0} ticks) (over {2:N0} iterations)",
                avgElapsedMillisecondsPerRun, avgElapsedTicksPerRun, iterations);
        }

        static void MapObjectsReduceAutoMapper()
        {
            List<FirstObject> firstObjects = new List<FirstObject>();
            List<MapFirstObject> mapFirstObjects = new List<MapFirstObject>();

            ReducedAutoMapper.Instance.CreateMap<FirstObject, MapFirstObject>();
            ReducedAutoMapper.Instance.CreateMap<SecondObject, MapSecondObject>();
            ReducedAutoMapper.Instance.CreateMap<ThirdObject, MapThirdObject>();
            for (int i = 0; i < 10000; i++)
            {
                FirstObject firstObject =
                    new FirstObject(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), (decimal)12.2, DateTime.Now,
                        new SecondObject(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), (decimal)11.2));
                firstObjects.Add(firstObject);
            }
            foreach (var currentObject in firstObjects)
            {
                MapFirstObject mapSecObj = ReducedAutoMapper.Instance.Map<FirstObject, MapFirstObject>(currentObject);
                mapFirstObjects.Add(mapSecObj);
            }
        }
    }
}
