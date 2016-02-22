using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fidely.Demo.Windows.Model
{
    public class Log
    {
        public string Category { get; set; }

        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public string Type { get; set; }

        public string Message { get; set; }


        public Log()
        {
        }

        public Log(string message)
        {
            var match = Regex.Match(message, @"^Fidely.Framework.Logger (?<Category>.+?): (?<Id>\d+?) : \[(?<Timestamp>.+?)\] \[(?<Type>.+?)\] (?<Message>.+)$");
            Category = match.Groups["Category"].Value;
            Id = Int32.Parse(match.Groups["Id"].Value);
            Timestamp = DateTimeOffset.Parse(match.Groups["Timestamp"].Value);
            Type = match.Groups["Type"].Value.Split('.').Last();
            Message = match.Groups["Message"].Value;
        }
    }
}
