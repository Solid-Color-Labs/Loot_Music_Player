using System.Windows.Media;
using GalaSoft.MvvmLight;
using MusicPlayer.EventArguments;
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
    public class CurrentlyPlayingViewModel : ViewModelBase
    {
        public CurrentlyPlayingViewModel()
        {
            SubscribeToPlayerEvent();
        }

        private void SubscribeToPlayerEvent()
        {
            MediaPlayer.TrackChanged += MediaPlayer_TrackChanged;
        }

        private ImageSource _albumImage;
        public ImageSource AlbumImage
        {
            get { return _albumImage; }
            private set { _albumImage = value; RaisePropertyChanged(); }
        }

        private string _trackTitle;
        public string TrackTitle
        {
            get { return _trackTitle; }
            private set { _trackTitle = value; RaisePropertyChanged(); }
        }

        private string _trackArtist;
        public string TrackArtist
        {
            get { return _trackArtist; }
            set { _trackArtist = value; RaisePropertyChanged(); }
        }

        /* When a track is changed, the properties will be set 
         * and then those properties will notify the view, which is bound to the property,
         * of the changes.
         */
        private void MediaPlayer_TrackChanged(object sender, NewTrackArgs e)
        {
            Track track = e.CurrentTrack;
            AlbumImage = track.ImageSource; 
            TrackTitle = track.Title; 
            TrackArtist = track.Artist; 
        }
    }
}