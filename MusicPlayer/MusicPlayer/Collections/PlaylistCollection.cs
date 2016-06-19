using System.Collections.Generic;
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
Date: 4/24/16
*/
namespace MusicPlayer.Collections
{
    /* This class is static, so that playlists can be 
     * accessed from any class in the application.
     * 
     * This class is a wrapper for a dictionary, with the key being 
     * the playlists title, and the value being a playlist object.
     */
public static class PlaylistCollection
    {
        public static Dictionary<string, Playlist> Playlists { get; }

        public static int Count => Playlists.Count;

        static PlaylistCollection()
        {
            Playlists = new Dictionary<string, Playlist>();
        }

        public static void AddPlaylist(Playlist playlist)
        {
            Playlists.Add(playlist.Title, playlist);
        }

        public static void RemovePlaylist(string name)
        {
            Playlists.Remove(name);
        }

        public static Playlist GetPlaylist(string name)
        {
            return Playlists[name];
        }

        public static bool HasPlaylist()
        {
            return Count > 0;
        }

        public static bool ContainsPlaylist(string name)
        {
            return !string.IsNullOrEmpty(name) && Playlists.ContainsKey(name);
        }

        public static ICollection<string> GetPlaylistNames()
        {
            return Playlists.Keys;
        }
    }
}