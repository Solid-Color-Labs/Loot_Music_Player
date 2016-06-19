using System.Collections.ObjectModel;
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
namespace MusicPlayer.Collections
{
    /* This class is static, so that all tracks can be 
     * accessed from any class in the application.
     * 
     * An ObservableCollection used, making it easy to 
     * bind all of the tracks to a view.
     */
    public static class TrackCollection
    {
        public static ObservableCollection<Track> Tracks { get; set; }
        public static int Count => Tracks.Count;

        static TrackCollection()
        {
            Tracks = new ObservableCollection<Track>();
        }

        public static void AddTrack(Track track)
        {
            Tracks.Add(track);
        }

        public static void RemoveTrack(int index)
        {
            Tracks.RemoveAt(index);
        }

        public static void RemoveAllTracks()
        {
            Tracks.Clear();
        }

        public static Track GetTrack(int index)
        {
            return Tracks[index];
        }

        public static bool HasTracks()
        {
            return Count > 0;
        }
    
    }
}