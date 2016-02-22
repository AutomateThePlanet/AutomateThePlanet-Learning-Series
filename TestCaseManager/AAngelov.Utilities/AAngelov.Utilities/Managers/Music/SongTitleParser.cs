using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AAngelov.Utilities.Managers.Music
{
    /// <summary>
    /// Contains methods for parsing song text
    /// </summary>
    public static class SongTitleParser
    {
        /// <summary>
        /// The regex song pattern
        /// </summary>
        private static readonly string RegexSongPattern = @"\s*(?<Artist>[a-zA-Z1-9\s\w]{1,})-(?<Name>[a-zA-Z1-9\-\s\w""']{1,})";

        /// <summary>
        /// Parses the title.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>the artist and the title of the text</returns>
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
