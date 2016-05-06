// <copyright file="YouTubeSong.cs" company="Automate The Planet Ltd.">
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

namespace YouTubePlaylistAPI
{
    public class YouTubeSong : BaseNotifyPropertyChanged, IYouTubeSong, IEquatable<YouTubeSong>
    {
        private string artist;

        private string title;

        private string songId;

        private string originalTitle;

        private Guid songGuid;

        public YouTubeSong()
        {
            this.SongGuid = Guid.NewGuid();
        }

        public YouTubeSong(
            string artist,
            string title,
            string originalTitle,
            string songId,
            string playlistItemId,
            ulong? duration)
            : this()
        {
            this.Artist = artist;
            this.Title = title;
            this.OriginalTitle = originalTitle;
            this.SongId = songId;
            this.PlayListItemId = playlistItemId;
            this.Duration = duration;
        }

        public YouTubeSong(IYouTubeSong iYouTubeSong)
        {
            this.Artist = iYouTubeSong.Artist;
            this.Title = iYouTubeSong.Title;
            this.SongId = iYouTubeSong.SongId;
            this.PlayListItemId = iYouTubeSong.PlayListItemId;
            this.Duration = iYouTubeSong.Duration;
            this.OriginalTitle = iYouTubeSong.OriginalTitle;
            this.SongGuid = iYouTubeSong.SongGuid;
        }

        public string PlayListItemId { get; set; }

        public ulong? Duration { get; set; }

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

        public string OriginalTitle
        {
            get
            {
                return this.originalTitle;
            }

            set
            {
                this.originalTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        public Guid SongGuid
        {
            get
            {
                return this.songGuid;
            }

            set
            {
                if (this.songGuid == default(Guid))
                {
                    this.songGuid = value;
                }
            }
        }

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

        public string Url
        {
            get
            {
                return string.Concat("https://www.youtube.com/watch?v=", this.SongId);
            }
        }

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

        public bool IsImported { get; set; }

        public bool Equals(YouTubeSong other)
        {
            return this.SongId.Equals(other.SongId);
        }
    }
}
