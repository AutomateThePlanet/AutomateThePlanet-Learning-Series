// <copyright file="EventLogAsserter.cs" company="Automate The Planet Ltd.">
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
using System.Linq;
using TopWindowsEventLogTips.Asserters;

namespace TopWindowsEventLogTips.Asserters
{
    public abstract class EventLogAsserter
    {
        private readonly string logName;
        private readonly List<string> machineNames;
        private readonly List<EventLog> eventLogs;
        private readonly List<EventLogEntry> revertedEventLogEntries;

        public EventLogAsserter(string logName, List<string> machineNames)
        {
            this.logName = logName;
            this.machineNames = machineNames;
            eventLogs = new List<EventLog>();
            revertedEventLogEntries = new List<EventLogEntry>();
        }

        public void AssertMessageExistsInTop(string message, string sourceName, int topCount)
        {
            var isMessagePresent = false;
            InitializeEventLogs(sourceName);
            for (var i = 0; i < topCount; i++)
            {
                if (revertedEventLogEntries.Count > i)
                {
                    var currentEntry = revertedEventLogEntries[i];
                    if (currentEntry.Message.Contains(message))
                    {
                        isMessagePresent = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            Assert.IsTrue(isMessagePresent, "The event log '{0}' doesn't contain the message '{1}'", logName, message);
        }

        private void InitializeEventLogs(string sourceName)
        {
            eventLogs.Clear();
            revertedEventLogEntries.Clear();
            foreach (var currentMachine in machineNames)
            {
                try
                {
                    var currentEventLog = new EventLog(logName, currentMachine, sourceName);
                    eventLogs.Add(currentEventLog);
                    var eventLogEntries = new EventLogEntry[currentEventLog.Entries.Count];
                    currentEventLog.Entries.CopyTo(eventLogEntries, 0);
                    revertedEventLogEntries.AddRange(eventLogEntries.Reverse().ToList());
                }
                catch (InvalidOperationException ex)
                {
                    var eventLogNotFoundExceptionMessage = string.Format("The specified event log- '{0}' doesn't exist on machine- '{1}'", logName, currentMachine);
                    throw new NonExistingEventLogException(eventLogNotFoundExceptionMessage, ex);
                }
            }
        }
    }
}
