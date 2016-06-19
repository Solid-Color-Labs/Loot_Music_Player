using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MusicPlayer.EventArguments;
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
Date: 4/26/16
*/
namespace MusicPlayer.ViewModel
{
    public class TrackControlViewModel : ViewModelBase
    {
        public RelayCommand TrackPositionCommand { get; private set; }
        public RelayCommand RepeatOnCommand { get; private set; }
        public RelayCommand RepeatOffCommand { get; private set; }
        public RelayCommand ShuffleOnCommand { get; private set; }
        public RelayCommand ShuffleOffCommand { get; private set; }

        public TrackControlViewModel()
        {
            InitCommands();
            SubscribeToEventHandlers();
        }

        private void InitCommands()
        {
            TrackPositionCommand = new RelayCommand(GoToTrackPosition, CanExecute);
            RepeatOnCommand = new RelayCommand(RepeatOn, CanExecute);
            RepeatOffCommand = new RelayCommand(RepeatOff, CanExecute);
            ShuffleOnCommand = new RelayCommand(ShuffleOn, CanExecute);
            ShuffleOffCommand = new RelayCommand(ShuffleOff, CanExecute);
        }

        private void SubscribeToEventHandlers()
        {
            MediaPlayer.TrackChanged += MediaPlayer_TrackChanged;
            MediaPlayer.TrackProgressChanged += MediaPlayer_TrackProgressChanged;
        }

        private int _currentPositon;
        public int CurrentPosition
        {
            get { return _currentPositon; }
            set { _currentPositon = value; RaisePropertyChanged(); }
        }

        private string _currentFormattedPosition;
        public string CurrentFormattedPosition
        {
            get { return _currentFormattedPosition; }
            private set { _currentFormattedPosition = value; RaisePropertyChanged(); }
        }

        private double _trackDuration;
        public double TrackDuration
        {
            get { return _trackDuration; }
            private set { _trackDuration = value; RaisePropertyChanged(); }
        }

        private string _trackFormattedDuration;
        public string TrackFormattedDuration
        {
            get { return _trackFormattedDuration; }
            private set { _trackFormattedDuration = value; RaisePropertyChanged(); }
        }

        public void GoToTrackPosition()
        {
            CurrentFormattedPosition = TimeUtil.FormatTimeSpan(CurrentPosition);
            MediaPlayer.SetTrackPosition(CurrentPosition);
        }

        private bool CanExecute()
        {
            return !MediaPlayer.IsStoppedOrUndefined();
        }

        private void RepeatOn()
        {
            MediaPlayer.RepeatTrack(true);
        }

        private void RepeatOff()
        {
            MediaPlayer.RepeatTrack(false);
        }

        private void ShuffleOn() 
        {
            MediaPlayer.ShuffleOn();
        }

        private void ShuffleOff()
        {
            MediaPlayer.ShuffleOff();
        }

        /* A time format of mm:ss, rather than m:ss is used, because
         * that is what the Windows Media Player returns.
         */
        private void MediaPlayer_TrackChanged(object sender, NewTrackArgs e)
        {
            CurrentFormattedPosition = "00:00";
            CurrentPosition = 0;
            Track track = e.CurrentTrack;
            TrackDuration = track.Duration.TotalSeconds; 
            TrackFormattedDuration = track.FormattedDuration; 
        }

        private void MediaPlayer_TrackProgressChanged(object sender, CurrentTrackPositionArgs e)
        {
            int currentPosition = e.CurrentPosition;
            CurrentPosition = currentPosition;
            CurrentFormattedPosition = e.CurrentPositionString; 
        }
    }
}