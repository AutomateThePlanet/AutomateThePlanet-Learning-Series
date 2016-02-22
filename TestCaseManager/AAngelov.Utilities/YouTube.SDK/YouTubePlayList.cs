using System;
using System.Linq;

namespace YouTube.SDK
{
    /// <summary>
    /// Represents YouTube Playlist
    /// </summary>
    public class YouTubePlayList
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubePlayList"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="id">The identifier.</param>
        public YouTubePlayList(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }
    }
}