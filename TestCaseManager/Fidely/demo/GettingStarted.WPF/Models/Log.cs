/*
 * Copyright 2011 Shou Takenaka
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Fidely.Demo.GettingStarted.WPF.Models
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
