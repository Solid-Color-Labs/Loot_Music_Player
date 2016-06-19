using System.Collections.Generic;
using MusicPlayer.Collections;
using MusicPlayer.Data;
using MusicPlayer.Extensions;
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
    /* This class is a utility class, for adding and removing track objects 
     * from a specific playlist. It is closly tied to the PlaylistCollection,
     * which deals with a collection of playlist objects.
     */
    public class PlaylistCollectionUtil
    {
        public static void AddTrackToPlaylist(string playlistName, Track track)
        {
            Playlist playlist = PlaylistCollection.GetPlaylist(playlistName);
            playlist.Tracks.Add(track);
        }

        public static void RemoveTrackFromPlaylist(string playlistName, int trackIndex) 
        {
            Playlist playlist = PlaylistCollection.GetPlaylist(playlistName);
            playlist.Tracks.RemoveAt(trackIndex);
        }

        public static void RemoveTrackFromAllPlaylists(Track track)
        {
            Dictionary<string, Playlist> playlists = PlaylistCollection.Playlists;
            foreach (KeyValuePair<string, Playlist> entry in playlists)
            {
                entry.Value.Tracks.RemoveAll(t => t.Id == track.Id); 
            }
        }

        public static void RemoveAllTracksFromAllPlaylists()
        {
            Dictionary<string, Playlist> playlists = PlaylistCollection.Playlists;
            foreach (KeyValuePair<string, Playlist> entry in playlists)
            {
                entry.Value.Tracks.Clear();
            }
        }

        public static void RemovePlaylist(Playlist playlist)
        {
            PlaylistDb.DeletePlaylist(playlist);
            PlaylistCollection.RemovePlaylist(playlist.Title);
        }
    }
}