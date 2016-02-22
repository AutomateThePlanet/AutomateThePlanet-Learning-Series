using System;
using System.Collections.Generic;
using System.Linq;
using YouTube.SDK.Entities.Enums;

namespace YouTube.SDK.Entities
{
    /// <summary>
    /// Used to sort songs in different modes
    /// </summary>
    public class SongsComparer : IComparer<YouTubeSong>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SongsComparer"/> class.
        /// </summary>
        /// <param name="mode">The mode.</param>
        public SongsComparer(SongsSortModes mode = SongsSortModes.Artist)
        {
            this.SongsSortMode = mode;
        }   

        /// <summary>
        /// Gets or sets the songs sort mode.
        /// </summary>
        /// <value>
        /// The songs sort mode.
        /// </value>
        public SongsSortModes SongsSortMode { get; set; }

        /// <summary>
        /// Compares the specified first song.
        /// </summary>
        /// <param name="firstSong">The first song.</param>
        /// <param name="secondSong">The second song.</param>
        /// <returns></returns>
        public int Compare(YouTubeSong firstSong, YouTubeSong secondSong)
        {
            switch (this.SongsSortMode)
            {

                case SongsSortModes.Name:
                    return firstSong.Title.CompareTo(secondSong.Title);
                default:
                case SongsSortModes.Artist:
                    return firstSong.Artist.CompareTo(secondSong.Artist);                    
            }
        }
    }
}
