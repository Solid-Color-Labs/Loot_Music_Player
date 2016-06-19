using System.Collections.ObjectModel;
using MusicPlayer.Collections;
using MusicPlayer.Data;
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
    /* This is a utility class for dealing
     * with the track collections this application uses.
     * Since all of the tracks the user has added are
     * stored differently from the tracks added to a playlist,
     * this class allows for one method call when a collection is needed,
     * rather than having if/else statments throught the aplication.
     */
    public static class TrackCollectionUtil
    {
        /* The "All" playlist refers to the TrackCollection, which contains
         * all of the tracks the user has added. This was done, because in
         * most places throughout the application, the TrackCollection is
         * used in the same place as a collection from a playlist,
         * because an ObservableCollection is a collection specifially designed
         * for being bound to views.
         */
        public static ObservableCollection<Track> GetCollection(string playlistName)
        {
            return playlistName.Equals("All") ? TrackCollection.Tracks : PlaylistCollection.GetPlaylist(playlistName).Tracks;
        }

        public static Track GetTrack(string playlistName, int trackIndex)
        {
            var collection = GetCollection(playlistName);
            return collection[trackIndex];
        }

        public static bool HasTracks(string playlistName)
        {
            if (playlistName.Equals("All"))
            {
                return TrackCollection.HasTracks();
            }
            return PlaylistCollection.GetPlaylist(playlistName).Tracks.Count > 0;
        }

        public static void RemoveTrack(Track track, int index)
        {
            TrackCollection.RemoveTrack(index);
            TrackDb.RemoveTrack(track);
            PlaylistCollectionUtil.RemoveTrackFromAllPlaylists(track);
        }

        public static void RemoveAllTracks()
        {
            TrackCollection.RemoveAllTracks();
            PlaylistCollectionUtil.RemoveAllTracksFromAllPlaylists();
            TrackDb.RemoveAllTracks();
        }
    }
}