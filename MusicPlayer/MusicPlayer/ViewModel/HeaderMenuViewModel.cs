using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MusicPlayer.Collections;
using MusicPlayer.CollectionUtils;
using MusicPlayer.Data;
using MusicPlayer.Model;
using MusicPlayer.Utils;

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
Date: 4/22/16
*/
namespace MusicPlayer.ViewModel
{
    public class HeaderMenuViewModel : ViewModelBase
    {
        public RelayCommand AddTracksCommand { get; private set; }
        public RelayCommand RemoveTrackCommand { get; private set; }
        public RelayCommand RemoveAllTracksCommand { get; private set; }

        private int SelectedTrackIndex { get; set; }

        // Initialized to all, because it's the default playlist
        public string SelectedPlaylistName { get; set; } = "All";

        public HeaderMenuViewModel()
        {
            InitCommands();
            RegisterMessanger();
        }

        private void InitCommands()
        {
            AddTracksCommand = new RelayCommand(AddTracks);
            RemoveTrackCommand = new RelayCommand(RemoveTrack, CanRemoveTrack);
            RemoveAllTracksCommand = new RelayCommand(RemoveAllTracks, CanExecute);
        }

        /* To keep ViewModels decoupled, both
         * the name and index property are registerd via
         * a listener, rather than retrieved from each
         * respective viewmodel.
         */
        private void RegisterMessanger()
        {
            MessengerInstance.Register<PropertyChangedMessage<int>>(this, SelectedTrackIndexChanged);
            MessengerInstance.Register<PropertyChangedMessage<string>>(this, SelectedPlaylistChanged);
        }

        private void SelectedTrackIndexChanged(PropertyChangedMessage<int> propertyDetails)
        {
            if (propertyDetails.PropertyName == "SelectedTrackIndex")
            {
                SelectedTrackIndex = propertyDetails.NewValue;
            }
        }

        private void SelectedPlaylistChanged(PropertyChangedMessage<string> propertyDetails)
        {
            if (propertyDetails.PropertyName == "SelectedPlaylistName")
            {
                SelectedPlaylistName = propertyDetails.NewValue;
            }
        }

        public void AddTracks()
        {
            string[] paths = FileIO.GetFilePathsFromDialog();
            if (paths != null && paths.Length != 0)
            {
                List<Track> tracks = new List<Track>();
                foreach (var path in paths)
                {
                    Track track = TrackUtil.CreateTrack(path);
                    AddToTrackCollection(track);
                    tracks.Add(track);
                }
                AddTracksToDatabase(tracks);
            }
        }

        private void AddToTrackCollection(Track track)
        {
            TrackCollection.AddTrack(track);
        }

        private void AddTracksToDatabase(ICollection<Track> tracks)
        {
            TrackDb.AddTracks(tracks);
        }

        public void RemoveTrack()
        {
            Track track = TrackCollection.GetTrack(SelectedTrackIndex);
            TrackCollectionUtil.RemoveTrack(track, SelectedTrackIndex);
        }

        /* The user can only remove tracks from the application, if
         * they are on the default playlist (all tracks). For dealing with collections
         * the 'all tracks' collection is treated as a collection of a playlist,
         * with the difference being, that the playlist doesn't actually exists anywhere,
         * only the collection.
         */
        public bool CanRemoveTrack()
        {
            return SelectedPlaylistName == "All" && SelectedTrackIndex >= 0 && CanExecute();
        }

        public bool CanExecute()
        {
            return TrackCollection.HasTracks();
        }

        /* When the user removes all tracks, he is brought to
         * the default view. To do so, the playlist name of "all"
         * is sent out, notifying the registered listeners of the change.
         */
        public void RemoveAllTracks()
        {
            TrackCollectionUtil.RemoveAllTracks();
            SendSelectedPlaylistName("All");
        }

        private void SendSelectedPlaylistName(string selectedPlaylistName)
        {
            MessengerInstance.Send(new PropertyChangedMessage<string>(string.Empty, selectedPlaylistName, "SelectedPlaylistName"));
        }
    }
}