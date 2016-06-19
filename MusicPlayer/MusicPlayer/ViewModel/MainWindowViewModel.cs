using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
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
namespace MusicPlayer.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand LoadCommand { get; private set; }

        public MainWindowViewModel()
        {
            InitCommand();
        }

        private void InitCommand()
        {
            LoadCommand = new RelayCommand(LoadAppData);
        }

        private void LoadAppData()
        {
            InitTrackCollections();
            InitPlaylistCollections();
        }

        private void InitTrackCollections()
        {
            IEnumerable<Track> tracks = TrackDb.GetAllTracks();
            foreach (var track in tracks)
            {
                TrackCollection.AddTrack(track);
            }
        }

        private void InitPlaylistCollections()
        {
            SendPlaylists();
            InitPlaylists();
        }

        private void SendPlaylists()
        {
            List<string> playListNames = PlaylistDb.GetAllPlaylistNames();
            MessengerInstance.Send(new PropertyChangedMessage<ObservableCollection<string>>(null, new ObservableCollection<string>(playListNames), "ObservablePlaylists")); 
        }

        private void InitPlaylists()
        {
            IEnumerable<Playlist> dbPlaylists = PlaylistDb.GetAllPlaylists();
            foreach (var playlist in dbPlaylists)
            {
                PlaylistCollection.AddPlaylist(playlist); 
            }
        }
    }
}