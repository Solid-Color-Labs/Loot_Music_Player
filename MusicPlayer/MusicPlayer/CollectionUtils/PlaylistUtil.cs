using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using MusicPlayer.Collections;
using MusicPlayer.Model;

/*
Copyright 2016 Oliver Klesing

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

Author: Oliver Klesing
Date: 4/27/16
*/
namespace MusicPlayer.CollectionUtils
{
    /* This is a utility class for 
     * creating a playlist object.
     */
    public static class PlaylistUtil
    {
        private static int PlaylistId { get; set; }
        static PlaylistUtil()
        {
            PlaylistId = GetMaxId();
        }

        /* The database auto increments the id by 1.
         * To keep a new playlist's id property in synch
         * with the database, when the object is created,
         * this method finds the highest id, so that
         * a new playlist can increment the value from
         * the highest id. 
         */
        private static int GetMaxId()
        {
            return !PlaylistCollection.HasPlaylist() ? 1 : PlaylistCollection.Playlists.Max(p => p.Value.Id);
        }

        public static Playlist CreatePlaylist(string title)
        {
            CheckId();
            PlaylistId++;
            Playlist playlist = new Playlist
            {
                Id = PlaylistId,
                Title = title,
                Tracks = new ObservableCollection<Track>()
            };
            return playlist;
        }

        private static void CheckId()
        {
            if (!PlaylistCollection.HasPlaylist())
            {
                PlaylistId = 1;
            }
        }
    }
}