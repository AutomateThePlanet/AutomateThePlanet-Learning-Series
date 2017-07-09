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
            SongGuid = Guid.NewGuid();
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
            Artist = artist;
            Title = title;
            OriginalTitle = originalTitle;
            SongId = songId;
            PlayListItemId = playlistItemId;
            Duration = duration;
        }

        public YouTubeSong(IYouTubeSong iYouTubeSong)
        {
            Artist = iYouTubeSong.Artist;
            Title = iYouTubeSong.Title;
            SongId = iYouTubeSong.SongId;
            PlayListItemId = iYouTubeSong.PlayListItemId;
            Duration = iYouTubeSong.Duration;
            OriginalTitle = iYouTubeSong.OriginalTitle;
            SongGuid = iYouTubeSong.SongGuid;
        }

        public string PlayListItemId { get; set; }

        public ulong? Duration { get; set; }

        public string Artist
        {
            get
            {
                return artist;
            }

            set
            {
                artist = value;
                NotifyPropertyChanged();
            }
        }

        public string OriginalTitle
        {
            get
            {
                return originalTitle;
            }

            set
            {
                originalTitle = value;
                NotifyPropertyChanged();
            }
        }

        public Guid SongGuid
        {
            get
            {
                return songGuid;
            }

            set
            {
                if (songGuid == default(Guid))
                {
                    songGuid = value;
                }
            }
        }

        public string SongId
        {
            get
            {
                return songId;
            }

            set
            {
                songId = value;
                NotifyPropertyChanged();
            }
        }

        public string Url
        {
            get
            {
                return string.Concat("https://www.youtube.com/watch?v=", SongId);
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsImported { get; set; }

        public bool Equals(YouTubeSong other)
        {
            return SongId.Equals(other.SongId);
        }
    }
}
