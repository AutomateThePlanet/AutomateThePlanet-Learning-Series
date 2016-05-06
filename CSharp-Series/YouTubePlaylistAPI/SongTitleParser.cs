// <copyright file="SongTitleParser.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YouTubePlaylistAPI
{
    public static class SongTitleParser
    {
        private static readonly string RegexSongPattern = @"\s*(?<Artist>[a-zA-Z1-9\s\w]{1,})-(?<Name>[a-zA-Z1-9\-\s\w""']{1,})";

        public static KeyValuePair<string, string> ParseTitle(string text)
        {
            Regex regexNamespaceInitializations = new Regex(RegexSongPattern, RegexOptions.None);

            Match m = regexNamespaceInitializations.Match(text);
            KeyValuePair<string, string> currentSong = default(KeyValuePair<string, string>);
            if (m.Success)
            {
                currentSong = new KeyValuePair<string, string>(m.Groups["Artist"].ToString(), m.Groups["Name"].ToString());
            }
            else
            {
                currentSong = new KeyValuePair<string, string>(text, string.Empty);
            }

            return currentSong;
        }
    }
}
