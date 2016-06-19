using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
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
Date: 4/21/16
*/

//Every ViewModel in MVVM Light Toolkit must inherit from ViewModelBase class.
namespace MusicPlayer.ViewModel
{
    public class TrackViewModel : ViewModelBase
    {
        public RelayCommand<int> PlayTrackCommand { get; private set; }
        public RelayCommand RemoveFromPlaylistCommand { get; private set; }
        public RelayCommand<string> AddToPlaylistsCommand{ get; private set; }
        public RelayCommand RefreshPlaylistCollectionCommand { get; private set; }

        private int _selectedTrackIndex;
        public int SelectedTrackIndex
        {
            get { return _selectedTrackIndex; }
            set {
                _selectedTrackIndex = value;
                RaisePropertyChanged();
                SendSelectedTrackIndex(value);
            }
        }

        /* The selected track index is sent whenever it changes,
         * ensuring that ViewModels dependent on a track index are decoupled
         * from this ViewModel.
         */
        private void SendSelectedTrackIndex(int index)
        {
            MessengerInstance.Send(new PropertyChangedMessage<int>(0, index, "SelectedTrackIndex"));
        }

        private ObservableCollection<Track> _observableTracks;
        public ObservableCollection<Track> ObservableTracks
        {
            get { return _observableTracks; }
            private set
            {
                _observableTracks = value;
                RaisePropertyChanged();
                _isNewPlaylist = true;
            }
        }

        /* CollectionViewSource is used, ensuring that the control which
         * is bound to this property will always have the latest list
         * of playlist names.
         */
        public ICollectionView PlaylistCollectionView => CollectionViewSource.GetDefaultView(PlaylistCollection.GetPlaylistNames());

        /* Property is initialized to "all", because 
         * that's the default Playlist. It contains
         * all tracks the user has added to the player.
         */
        public string SelectedPlaylistName { get; set; } = "All";

        private bool _isNewPlaylist;

        public TrackViewModel()
        {
            InitCommands();
            RegisterMessangers();
            InitCollection();
        }

        /* The relay command is part of the MVVM light framework.
         * Rather than creating a sepperate class for each command
         * and implementing the ICommand interface, it
         * allows for a command to be a property.
         */
        private void InitCommands()
        {
            PlayTrackCommand = new RelayCommand<int>(Play, CanPlayTrack);
            RemoveFromPlaylistCommand = new RelayCommand(RemoveFromPlaylist, CanRemoveFromPlaylist);
            AddToPlaylistsCommand = new RelayCommand<string>(AddToPlaylist, CanAddToPlaylist);
            RefreshPlaylistCollectionCommand = new RelayCommand(RefreshPlaylistCollection);
        }

        /* Makes sure that if the user adds a new playlist, it shows
         * up in the CollectionView.
         */
        private void RefreshPlaylistCollection()
        {
            PlaylistCollectionView.Refresh();
        }

        /* Messangers are used as part of the MVVM light framework.
         * The MessangerInstance is inherited from ViewModelBase.
         * Messangers allow for decoupling of viewmodels, ensuring
         * one viewmodel won't be dependent on another.
         * To receive a message, all one has to do is register a listener.
         */
        private void RegisterMessangers() 
        {
            MessengerInstance.Register<PropertyChangedMessage<string>>(this, SelectedPlaylistChanged);
        }

        /*
         * The collection is initialized to the TrackCollection,
         * because it's the default collection.
         */
        private void InitCollection()
        {
            ObservableTracks = TrackCollection.Tracks;
        }

        /* This ViewModel deals with collections of tracks, which is why playlist changes 
         * are only sending the playlist name and not the entire track collection
         * of a playlist. This is why the track collection is retrieved and assigned to the
         * ObservableTracks property in this method.  
         */
        private void SelectedPlaylistChanged(PropertyChangedMessage<string> propertyDetails)
        {
            if (propertyDetails.PropertyName == "SelectedPlaylistName")
            {
                ObservableTracks = TrackCollectionUtil.GetCollection(propertyDetails.NewValue);
                SelectedPlaylistName = propertyDetails.NewValue;
            }
        }

        /* The SelectedItems.Count property of the TrackListView
         * is passed as a command parameter to both the action and the boolean validation
         * function, which is why both have an integer parameter, even though
         * this method isn't utilizing it.
         */
        private void Play(int selectedItemsCount)
        {
            CheckMediaPlayerPlaylist();
            MediaPlayer.Play(SelectedTrackIndex);
        }

        private void CheckMediaPlayerPlaylist()
        {
            if (_isNewPlaylist)
            {
                MediaPlayer.SetPlaylist(ObservableTracks);
                _isNewPlaylist = false;
            }
        }

        private bool CanPlayTrack(int selectedItemsCount)
        {
            bool isItemSelected = selectedItemsCount == 1;
            return isItemSelected;
        }

        private void RemoveFromPlaylist() 
        {
            RemoveFromDatabase();
            PlaylistCollectionUtil.RemoveTrackFromPlaylist(SelectedPlaylistName, SelectedTrackIndex);
        }

        private void RemoveFromDatabase()
        {
            Playlist playlist = PlaylistCollection.GetPlaylist(SelectedPlaylistName);
            Track track = playlist.Tracks[SelectedTrackIndex];
            PlaylistDb.DeleteTrackFromPlaylist(playlist, track);
        }

        private bool CanRemoveFromPlaylist()
        {
            return ObservableTracks.Count > 0 && SelectedTrackIndex >= 0 && SelectedPlaylistName != "All";
        }

        private void AddToPlaylist(string menuItem)
        {
           Track track = TrackCollectionUtil.GetTrack(SelectedPlaylistName, SelectedTrackIndex);
           PlaylistCollectionUtil.AddTrackToPlaylist(menuItem, track);
           Playlist playlist = PlaylistCollection.GetPlaylist(menuItem);
           PlaylistDb.AddTrackToPlaylist(playlist, track);
        }

        /* When a menu item is clicked, it is passed as a command parameter,
         * which is why both the action and the boolean validation function
         * have a string as a parameter, even though this function isn't utilizing
         * the parameter. 
         */
        private bool CanAddToPlaylist(string menuItem)
        {
            return SelectedTrackIndex >= 0;
        }
    }
}