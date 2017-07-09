// <copyright file="YouTubeServiceClient.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Diagnostics;


namespace YouTubePlaylistAPI
{
    public class YouTubeServiceClient
    {
        private static YouTubeServiceClient instance;

        public static YouTubeServiceClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new YouTubeServiceClient();
                }
                return instance;
            }
        }

        public List<IYouTubeSong> GetPlayListSongs(string userEmail, string playListId)
        {
            var playListSongs = new List<IYouTubeSong>();

            try
            {
                var service = new YouTubeServiceClient();
                service.GetPlayListSongsInternalAsync(userEmail, playListId, playListSongs).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                }
            }

            return playListSongs;
        }

        public bool RemoveSongFromPlaylist(string userEmail, string playlistItemId)
        {
            var isSuccessfullyRemoved = false;

            try
            {
                var service = new YouTubeServiceClient();
                service.RemoveSongFromPlaylistAsync(userEmail, playlistItemId).Wait();
                isSuccessfullyRemoved = true;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                    isSuccessfullyRemoved = false;
                }
            }

            return isSuccessfullyRemoved;
        }

        private async Task RemoveSongFromPlaylistAsync(string userEmail, string playlistItemId)
        {
            var youtubeService = await GetYouTubeService(userEmail);
            var deleteRequest = youtubeService.PlaylistItems.Delete(playlistItemId);
            var result = await deleteRequest.ExecuteAsync();
        }

        public bool AddSongToPlaylist(string userEmail, string songId, string playlistId)
        {
            var isSuccessfullyAdded = false;

            try
            {
                var service = new YouTubeServiceClient();
                service.AddSongToPlaylistAsync(userEmail, songId, playlistId).Wait();
                isSuccessfullyAdded = true;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                    isSuccessfullyAdded = false;
                }
            }

            return isSuccessfullyAdded;
        }

        public bool UpdateSongPositionInPlaylist(
            string userEmail,
            string playlistId,
            YouTubeSong song,
            int position)
        {
            var isSuccessfullyUpdated = false;

            try
            {
                var service = new YouTubeServiceClient();
                service.UpdatePlaylistItemAsync(userEmail, song.SongId, playlistId, song.PlayListItemId, position).Wait();
                isSuccessfullyUpdated = true;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                    isSuccessfullyUpdated = false;
                }
            }

            return isSuccessfullyUpdated;
        }

        public List<YouTubePlayList> GetUserPlayLists(string userEmail)
        {
            var playLists = new List<YouTubePlayList>();

            try
            {
                var service = new YouTubeServiceClient();
                service.GetUserPlayListsAsync(userEmail, playLists).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                }
            }

            return playLists;
        }

        private async Task GetPlayListSongsInternalAsync(string userEmail, string playListId, List<IYouTubeSong> playListSongs)
        {
            var youtubeService = await GetYouTubeService(userEmail);

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;
            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var listRequest = youtubeService.PlaylistItems.List("contentDetails");
                listRequest.MaxResults = 50;
                listRequest.PlaylistId = playListId;
                listRequest.PageToken = nextPageToken;
                var response = await listRequest.ExecuteAsync();
                if (playListSongs == null)
                {
                    playListSongs = new List<IYouTubeSong>();
                }
                foreach (var playlistItem in response.Items)
                {
                    var videoR = youtubeService.Videos.List("snippet,contentDetails,status");
                    videoR.Id = playlistItem.ContentDetails.VideoId;
                    var responseV = await videoR.ExecuteAsync();
                    if (responseV.Items.Count > 0)
                    {
                        var parsedSong = SongTitleParser.ParseTitle(responseV.Items[0].Snippet.Title);
                        var duration = new DurationParser().GetDuration(responseV.Items[0].ContentDetails.Duration);
                        IYouTubeSong currentSong = new YouTubeSong(parsedSong.Key, parsedSong.Value, responseV.Items[0].Snippet.Title, responseV.Items[0].Id, playlistItem.Id, duration);
                        playListSongs.Add(currentSong);
                        Debug.WriteLine(currentSong.Title);
                    }
                }
                nextPageToken = response.NextPageToken;
            }
        }

        private async Task GetUserPlayListsAsync(string userEmail, List<YouTubePlayList> playLists)
        {
            var youtubeService = await GetYouTubeService(userEmail);

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;
            var playlists = youtubeService.Playlists.List("snippet");
            playlists.PageToken = "";
            playlists.MaxResults = 50;
            playlists.Mine = true;
            var presponse = await playlists.ExecuteAsync();
            foreach (var currentPlayList in presponse.Items)
            {
                playLists.Add(new YouTubePlayList(currentPlayList.Snippet.Title, currentPlayList.Id));
            }
        }

        private async Task AddSongToPlaylistAsync(string userEmail, string songId, string playlistId)
        {
            var youtubeService = await GetYouTubeService(userEmail);
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = playlistId;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = songId;
            newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();
        }

        private async Task UpdatePlaylistItemAsync(string userEmail, string songId, string playlistId, string playlistItemId, int position)
        {
            var youtubeService = await GetYouTubeService(userEmail);
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = playlistId;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = songId;
            newPlaylistItem.Snippet.Position = position;
            newPlaylistItem.Id = playlistItemId;
            newPlaylistItem = await youtubeService.PlaylistItems.Update(newPlaylistItem, "snippet,contentDetails,status").ExecuteAsync();
        }

        private async Task<YouTubeService> GetYouTubeService(string userEmail)
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[]
                    { 
                        YouTubeService.Scope.Youtube,
                        YouTubeService.Scope.Youtubepartner,
                        YouTubeService.Scope.YoutubeUpload,
                        YouTubeService.Scope.YoutubepartnerChannelAudit, 
                        YouTubeService.Scope.YoutubeReadonly 
                    },
                    userEmail,
                    CancellationToken.None,
                    new FileDataStore(GetType().ToString()));
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GetType().ToString()
            });

            return youtubeService;
        }
    }
}
