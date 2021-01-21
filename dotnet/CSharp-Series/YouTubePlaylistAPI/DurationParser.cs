// <copyright file="DurationParser.cs" company="Automate The Planet Ltd.">
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
using System.Text.RegularExpressions;

namespace YouTubePlaylistAPI
{
    public class DurationParser
    {
        private readonly string durationRegexExpression = @"PT(?<minutes>[0-9]{0,})M(?<seconds>[0-9]{0,})S";

        public ulong? GetDuration(string durationStr)
        {
            var durationResult = default(ulong?);
            var regexNamespaceInitializations = new Regex(durationRegexExpression, RegexOptions.None);
            var m = regexNamespaceInitializations.Match(durationStr);
            if (m.Success)
            {
                var minutesStr = m.Groups["minutes"].Value;
                var secondsStr = m.Groups["seconds"].Value;
                var minutes = int.Parse(minutesStr);
                var seconds = int.Parse(secondsStr);
                var duration = new TimeSpan(0, minutes, seconds);
                durationResult = (ulong)duration.Ticks;
            }

            return durationResult;
        }
    }
}
