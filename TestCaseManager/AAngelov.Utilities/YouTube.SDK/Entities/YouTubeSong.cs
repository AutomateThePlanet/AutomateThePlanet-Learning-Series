using AAngelov.Utilities.UI.Core;
using Google.Apis.YouTube.v3.Data;
using YouTube.SDK.Entities.Contracts;

namespace YouTube.SDK.Entities
{
    /// <summary>
    /// YouTube Grooveshark Import Song Entity
    /// </summary>
    public class YouTubeSong : BaseNotifyPropertyChanged, IYouTubeSong
    {
        private string artist;

        private string title;

        private string songId;

        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubeSong" /> class.
        /// </summary>
        /// <param name="artist">You tube artist.</param>
        /// <param name="title">You tube song title.</param>
        /// <param name="songId">The song identifier.</param>
        /// <param name="playlistItemId">The playlist item identifier.</param>
        /// <param name="resourceId">The resource identifier.</param>
        public YouTubeSong(string artist, string title, string songId, string playlistItemId, ResourceId resourceId)
        {
            this.Artist = artist;
            this.Title = title;
            this.SongId = songId;
            this.PlayListItemId = playlistItemId;
            this.ResourceId = resourceId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="YouTubeSong"/> class.
        /// </summary>
        /// <param name="iYouTubeSong">The i you tube song.</param>
        public YouTubeSong(IYouTubeSong iYouTubeSong)
        {
            this.Artist = iYouTubeSong.Artist;
            this.Title = iYouTubeSong.Title;
            this.SongId = iYouTubeSong.SongId;
            this.PlayListItemId = iYouTubeSong.PlayListItemId;
        }

        /// <summary>
        /// Gets or sets the play list item identifier.
        /// </summary>
        /// <value>
        /// The play list item identifier.
        /// </value>
        public string PlayListItemId { get; set; }

        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        /// <value>
        /// The resource identifier.
        /// </value>
        public ResourceId ResourceId { get; set; }

        /// <summary>
        /// Gets or sets you tube artist.
        /// </summary>
        /// <value>
        /// You tube artist.
        /// </value>
        public string Artist
        {
            get
            {
                return this.artist;
            }

            set
            {
                this.artist = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the song identifier.
        /// </summary>
        /// <value>
        /// The song identifier.
        /// </value>
        public string SongId
        {
            get
            {
                return this.songId;
            }

            set
            {
                this.songId = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets you tube song title.
        /// </summary>
        /// <value>
        /// You tube song title.
        /// </value>
        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.NotifyPropertyChanged();
            }
        }       

        /// <summary>
        /// Gets or sets a value indicating whether this instance is imported.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is imported; otherwise, <c>false</c>.
        /// </value>
        public bool IsImported { get; set; }      
    }
}