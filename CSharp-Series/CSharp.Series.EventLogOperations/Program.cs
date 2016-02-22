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
            EventLog eventLog = new EventLog();
            eventLog.Source = "Automate The Planet";

            // Write a new entry to the source.
            eventLog.WriteEntry("You need community. It’s here and it’s waiting just for you. ", EventLogEntryType.Information);

            // Reading Entries
            EventLog automateThePlanetLog = new EventLog();
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
