using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using MusicPlayer.Collections;
using MusicPlayer.CollectionUtils;
using MusicPlayer.EventArguments;
using MusicPlayer.Model;
using WMPLib;

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
Date: 4/25/16
*/
namespace MusicPlayer.ViewModel
{
    public class MusicControlViewModel : ViewModelBase
    {
        public RelayCommand PlayTrackCommand { get; private set; }
        public RelayCommand PlayTrackWhenStoppedCommand { get; private set; }
        public RelayCommand PauseTrackCommand { get; private set; }
        public RelayCommand NextTrackCommand { get; private set; }
        public RelayCommand PreviousTrackCommand { get; private set; }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { _isChecked = value; RaisePropertyChanged(); }
        }

        private int SelectedTrackIndex { get; set; }

        //Initialized to default playlist
        public string SelectedPlaylistName { get; set; } = "All";

        public MusicControlViewModel()
        {
            InitCommands();
            SubscribeToPlayerEvent();
            RegisterMessanger();
        }

        private void InitCommands()
        {
            PlayTrackCommand = new RelayCommand(Play, CanPlayOrPause);
            PlayTrackWhenStoppedCommand = new RelayCommand(PlayFirstTrack, CanPlayFirstTrack);
            PauseTrackCommand = new RelayCommand(Pause, CanPlayOrPause);
            NextTrackCommand = new RelayCommand(Next, CanExecute);
            PreviousTrackCommand = new RelayCommand(Previous, CanExecute);
        }

        private void SubscribeToPlayerEvent()
        {
            MediaPlayer.MediaStateChanged += MediaPlayer_StateChanged;
        }

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

        private void Play()
        {
            MediaPlayer.Play();
        }

        private bool CanPlayOrPause()
        {
            return !MediaPlayer.IsStoppedOrUndefined();
        }

        /* This method is executed when there 
         * is no track that is currently no playing/paused in the media player.
         * 
         * If the user presses play, and hasn't selected a track, the 
         * first track in the default playlist is played.
         */
        private void PlayFirstTrack()
        {
            bool isTrackSelected = SelectedTrackIndex >= 0;
            if (isTrackSelected)
            {
                SetMediaPlayerPlaylist();
                MediaPlayer.Play(SelectedTrackIndex);
            }
            else
            {
                SetDefaultPlaylist();
                MediaPlayer.Play(0);
            }
        }

        private void SetMediaPlayerPlaylist()
        {
            IList<Track> tracks = TrackCollectionUtil.GetCollection(SelectedPlaylistName);
            MediaPlayer.SetPlaylist(tracks);
        }

        private void SetDefaultPlaylist()
        {
            IList<Track> tracks = TrackCollection.Tracks;
            MediaPlayer.SetPlaylist(tracks);
        }

        private bool CanPlayFirstTrack()
        {
            return MediaPlayer.IsStoppedOrUndefined() && TrackCollectionUtil.HasTracks(SelectedPlaylistName);
        }

        private void Pause()
        {
            MediaPlayer.Pause();
        }

        private void Previous()
        {
            MediaPlayer.Previous();
        }

        private void Next()
        {
            MediaPlayer.Next();
        }

        private bool CanExecute()
        {
            return !MediaPlayer.IsStoppedOrUndefined() && MediaPlayer.TrackCount > 0;
        }

        /* Since the media player is a static class, any class
         * in the application could change the Media Player state.
         * This event makes sure that the UI
         * is in sync with the media player state.
         */
        private void MediaPlayer_StateChanged(object sender, MediaStateArgs e)
        {
            bool isStopped = WMPPlayState.wmppsStopped == e.State;
            if (isStopped)
            {
                IsChecked = false;
                return;
            }
            bool isPlaying = WMPPlayState.wmppsPlaying == e.State;
            if (isPlaying)
            {
                IsChecked = true;
            }
        }
    }
}