// <copyright file="program.cs" company="Automate The Planet Ltd.">
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

namespace CSharp.Series.EventLogOperations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Create Event Source if not exists.
            if (!EventLog.SourceExists("Automate The Planet"))
            {
                EventLog.CreateEventSource("Rock'N'Roll", "Automate The Planet");
            }
            var eventLog = new EventLog();
            eventLog.Source = "Automate The Planet";

            // Write a new entry to the source.
            eventLog.WriteEntry("You need community. It’s here and it’s waiting just for you. ", EventLogEntryType.Information);

            // Reading Entries
            var automateThePlanetLog = new EventLog();
            automateThePlanetLog.Log = "Automate The Planet";
            foreach (EventLogEntry entry in automateThePlanetLog.Entries)
            {
                Console.WriteLine(entry.Message);
            }

            // Delete Event Source if exists.
            if (EventLog.SourceExists("Rock'N'Roll"))
            {
                EventLog.DeleteEventSource("Rock'N'Roll");
            }

            // Delete the Log
            if (EventLog.Exists("Automate The Planet"))
            {
                // Delete Source
                EventLog.Delete("Automate The Planet");
            }
        }
    }
}