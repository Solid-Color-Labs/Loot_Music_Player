using System.Collections.ObjectModel;
using System.ComponentModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MusicPlayer.Collections;
using MusicPlayer.CollectionUtils;
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
Date: 4/22/16
*/
namespace MusicPlayer.ViewModel
{
    public class PlaylistViewModel : ViewModelBase, IDataErrorInfo
    {
        private string _playlistTextBoxText;
        public string PlaylistTextBoxText
        {
            get { return _playlistTextBoxText; }
            set
            {
                _playlistTextBoxText = value;
                RaisePropertyChanged();
            }
        }

        private int _selectedPlaylistIndex;
        public int SelectedPlaylistIndex
        {
            get { return _selectedPlaylistIndex; }
            set { _selectedPlaylistIndex = value; RaisePropertyChanged(); }
        }

        private string _selectedPlaylistName;
        public string SelectedPlaylistName
        {
            get { return _selectedPlaylistName; }
            set { _selectedPlaylistName = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<string> _observablePlaylists;
        public ObservableCollection<string> ObservablePlaylists
        {
            get { return _observablePlaylists; }
            private set
            {
                _observablePlaylists = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand CreatePlaylistCommand { get; private set; }
        public RelayCommand SetCurrentPlaylistCommand { get; private set; }
        public RelayCommand DeletePlaylistCommand { get; private set; }
        public RelayCommand SetAllTracksCommand { get; private set; }

        private bool IsValidPlaylistName { get; set; }

        public PlaylistViewModel()
        {
            InitProperties();
            InitCommands();
            RegisterMessanger();
        }

        // Selected Index is -1 if no playlist is selected
        private void InitProperties()
        {
            SelectedPlaylistIndex = -1;
        }

        private void InitCommands()
        {
            CreatePlaylistCommand = new RelayCommand(CreatePlaylist);
            SetCurrentPlaylistCommand = new RelayCommand(SetPlayList, AreAllTracksSelected);
            DeletePlaylistCommand = new RelayCommand(DeletePlaylist, AreAllTracksSelected);
            SetAllTracksCommand = new RelayCommand(SetAllTracks, AreAllTracksSelected);
        }

        private void RegisterMessanger() 
        {
            MessengerInstance.Register<PropertyChangedMessage<ObservableCollection<string>>>(this, InitPlaylists);
        }

        //Messanger is unregistered after being used once, as it is only used for initialization.
        private void InitPlaylists(PropertyChangedMessage<ObservableCollection<string>> propertyDetails)
        {
            if (propertyDetails.PropertyName == "ObservablePlaylists")
            {
                ObservablePlaylists = propertyDetails.NewValue;
                MessengerInstance.Unregister(this);
            }
        }

        private void CreatePlaylist() 
        {
            if (!string.IsNullOrEmpty(PlaylistTextBoxText) && IsValidPlaylistName)
            {
                ObservablePlaylists.Add(PlaylistTextBoxText);
                Playlist playlist = PlaylistUtil.CreatePlaylist(PlaylistTextBoxText);
                PlaylistCollection.AddPlaylist(playlist);
                PlaylistDb.AddPlaylist(playlist);
            }
        }

        /* 'All tracks' is the collection containing all of the tracks
         * the user added, and thus is not cosidered a playlist, which is
         * why the index is set to -1. 
         * It is however treated as a playlist when it comes to
         * retrieving a track collection.
         */
        private void SetAllTracks() 
        {
            SelectedPlaylistIndex = -1;
            SendSelectedPlaylistName("All");
        }

        private void SendSelectedPlaylistName(string selectedPlaylistName)
        {
            MessengerInstance.Send(new PropertyChangedMessage<string>(string.Empty, selectedPlaylistName, "SelectedPlaylistName"));
        }

        private bool AreAllTracksSelected() 
        {
            return SelectedPlaylistIndex != -1;
        }

        private void SetPlayList()
        {
            SendSelectedPlaylistName(SelectedPlaylistName);
        }

        /* After a playlist has been deleted
         * the default playlist is set.
         */
        private void DeletePlaylist() 
        {
            Playlist playlist = GetSelectedPlaylist();
            PlaylistCollectionUtil.RemovePlaylist(playlist);
            ObservablePlaylists.RemoveAt(SelectedPlaylistIndex);
            SetAllTracks();
        }

        private Playlist GetSelectedPlaylist()
        {
            string playlistName = ObservablePlaylists[SelectedPlaylistIndex];
            return PlaylistCollection.GetPlaylist(playlistName);
        }

        /* The code below deals with datavalidation, as part of
         * the PlaylistTextBox's ValidatesOnDataErrors=True property.
         * 
         * Returning null, means the data passes validation.
         */
        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = null;
                if (propertyName == "PlaylistTextBoxText")
                {
                    error = ValidatePlaylistName(PlaylistTextBoxText);
                }
                return error;
            }
        }

        /* No duplicate playlist names are allowed, because
         * the collection containing the playlists is a 
         * dictionary, and it cannot have duplicate keys.
         * 
         * If return value is null, it means it passes data validation.
         */
        private string ValidatePlaylistName(string name)
        {
            IsValidPlaylistName = !PlaylistCollection.ContainsPlaylist(name);
            return IsValidPlaylistName ? null : "Playlist already exists";
        }

        string IDataErrorInfo.Error => null;
    }
}