using System;
using System.Collections.Generic;
using System.Windows.Threading;
using MusicPlayer.EventArguments;
using MusicPlayer.Extensions;
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
Date: 4/27/16
*/
namespace MusicPlayer
{
    /* This class handles everything pertaining to
     * playing music. From playing music, to shuffling a playlist,
     * to keeping track of time. 
     */
    public static class MediaPlayer
    {
        public static event EventHandler<CurrentTrackPositionArgs> TrackProgressChanged;
        public static event EventHandler<NewTrackArgs> TrackChanged;
        public static event EventHandler<MediaStateArgs> MediaStateChanged;

        /* Event arguments for the event handlers are declared as fields.
         * This was done, because the arguments get updated quite often.
         * Having them as fields, means a new instance doesn't have to be created 
         * every time. For instance TrackProgressChanged gets updated every second. 
         * This would mean it instantiating a new object every second.
         */
        private static CurrentTrackPositionArgs _currentTrackPositionArgs;
        private static MediaStateArgs _currentMediaPlayerStateArgs;
        private static NewTrackArgs _newTrackArgs;

        /* Songs are played through the Windows Media Player,
         * which comes installed on every windows machine.
         */
        private static WindowsMediaPlayer _wmPlayer;

        /*
        The DispatcherTimer is used to keep track of one second. 
        Its only purpose is calling the tick event every second.
        */
        private static DispatcherTimer _dispatcherTimer;
        private static bool _isRepeat;

        // Before tracks are sorted, they are stored in an unsorted list.
        private static IList<Track> _currentPlaylist;
        private static IList<Track> _unsortedPlaylist;

        private static int _currentTrackIndex;

        public static int TrackCount => _currentPlaylist.Count;

        static MediaPlayer()
        {
            InitMediaPlayer();
            InitTrackInformation();
            InitDispatcherTimer();
            InitEventArgs();
        }

        private static void InitMediaPlayer()
        {
            _wmPlayer = new WindowsMediaPlayer();
            _wmPlayer.PlayStateChange += Player_PlayStateChange;
        }

        private static void InitTrackInformation()
        {
            _currentTrackIndex = 0;
            _isRepeat = false;
        }

        // Timer that calles event at 1 second intervals.
        private static void InitDispatcherTimer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

        private static void InitEventArgs()
        {
            _currentTrackPositionArgs = new CurrentTrackPositionArgs();
            _currentMediaPlayerStateArgs = new MediaStateArgs();
            _newTrackArgs = new NewTrackArgs();
        }

        /*
        This method starts the timer. If the timer is running
        when calling timer.start(), the timer will restart.   
        */
        private static void StartTimer()
        {
            _dispatcherTimer.Start();
        }

        public static void SetPlaylist(IList<Track> playlist)
        {
            _currentPlaylist = playlist;
        }

        public static void Play(int trackIndex)
        {
            PlaySpecificTrack(trackIndex);
            _currentTrackIndex = trackIndex;
            OnTrackChanged(); 
            StartTimer();
        }

        private static void PlaySpecificTrack(int trackIndex)
        {
            Track track = _currentPlaylist[trackIndex];
            _wmPlayer.URL = track.Path;
            _wmPlayer.controls.play();
        }

        public static void Play()
        {
            _wmPlayer.controls.play();
            StartTimer();
        }

        public static void Pause()
        {
            _wmPlayer.controls.pause();
            StopTimer();
        }

        private static void StopTimer()
        {
            _dispatcherTimer.Stop();
        }

        public static void Next()
        {
            PlayNextTrack();
            OnTrackChanged(); 
            StartTimer();
        }

        private static void PlayNextTrack()
        {
            IncrementCurrentTrackIndex();
            PlaySpecificTrack(_currentTrackIndex);
        }

        /* If the index is higher than the number of items in the
         * playlist, it gets reset to 0. This is done so that the first track
         * is played, when the user presses next on the last track.
         */
        private static void IncrementCurrentTrackIndex()
        {
            if (_currentTrackIndex < GetHighestIndexInPlaylist())
            {
                _currentTrackIndex++;
            }
            else
            {
                _currentTrackIndex = 0;
            }
        }

        private static int GetHighestIndexInPlaylist()
        {
            return _currentPlaylist.Count - 1;
        }

        public static void Previous()
        {
            PlayPreviousTrack();
            OnTrackChanged(); 
            StartTimer();
        }

        private static void PlayPreviousTrack()
        {
            DecrementCurrentTrackIndex(); 
            PlaySpecificTrack(_currentTrackIndex);
        }

        /* This method exists for the same reasons IncrementCurrentTrackIndex
         * does. This method just does the opposite.
         */
        private static void DecrementCurrentTrackIndex()
        {
            if (_currentTrackIndex > 0)
            {
                _currentTrackIndex--;
            }
            else
            {
                _currentTrackIndex = GetHighestIndexInPlaylist();
            }
        }

        public static void SetTrackPosition(int position)
        {
            _wmPlayer.controls.currentPosition = position;
        }

        public static bool IsStoppedOrUndefined()
        {
            return _wmPlayer.playState == WMPPlayState.wmppsStopped || _wmPlayer.playState == WMPPlayState.wmppsUndefined;
        }

        public static void RepeatTrack(bool repeat)
        {
            _isRepeat = repeat;
        }

        /* A new list is created, via a shallow copy from the current playlist, 
         * when this method is called, because all collections are passed by ref.
         * If I where not done, then both the _unsortedPlaylist and _currentPlaylist would
         * point to the same collection, meaning that sorting the collection would
         * affter both variables.
         */
        public static void ShuffleOn() 
        {
            _unsortedPlaylist = _currentPlaylist;
            _currentPlaylist = new List<Track>(_currentPlaylist);
            _currentPlaylist.Shuffle();
        }

        /* When this method is called, the _unsortedPlaylist is assigned
         * null, as it's not needed anymore, as the _currentPlaylist will be unsorted.
         */
        public static void ShuffleOff()
        {
            _currentPlaylist = _unsortedPlaylist;
            _unsortedPlaylist = null;
        }

        private static void Player_PlayStateChange(int newState)
        {
            /* wmppsMediaEnded is only called when the user doesn't click 
             * next or previous.
             */
            if ((WMPPlayState) newState == WMPPlayState.wmppsMediaEnded)
            {
                bool hasTracksInPlaylist = TrackCount > 0;
                if (hasTracksInPlaylist)
                {
                    if (!_isRepeat)
                    {
                        PlayNextTrackOnNewThread();
                    }
                    else
                    {
                        PlaySameTrackOnNewThread();
                    }
                }
            }
            OnMediaStateChanged((WMPPlayState)newState);
        }

        /* This method calls a thread timer, which is called once, and then disposed.
         * It executes a task, in this case the next() method, on a sepperate thread.
         * This is done, because otherwise the media player won't play the next track and just stop, 
         * because the media player isn't in the "ready" state yet. Starting a new thread allows us
         * to not worry about the previous Media Player state once a song has finished playing (state ended).
         * 
         * When instantiating the timer object, a deligate is used as the first parameter.
         */
        private static void PlayNextTrackOnNewThread()
        {
            System.Threading.Timer threadTimer = null;
            threadTimer = new System.Threading.Timer(obj =>
            {
                Next();
                threadTimer.Dispose();
            }, null, 0, System.Threading.Timeout.Infinite);
        }

        // This method is used for the same reason as PlayNextTrackAfterDelay.
        private static void PlaySameTrackOnNewThread()
        {
            System.Threading.Timer threadTimer = null;
            threadTimer = new System.Threading.Timer(obj =>
            {
                PlaySpecificTrack(_currentTrackIndex);
                StartTimer();
                threadTimer.Dispose();
            }, null, 50, System.Threading.Timeout.Infinite);
        }

        private static void OnMediaStateChanged(WMPPlayState state) 
        {
            if (MediaStateChanged != null)
            {
                _currentMediaPlayerStateArgs.State = state;
                MediaStateChanged(null, _currentMediaPlayerStateArgs);
            }
        }

        private static void OnSongProgressChanged(int position)
        {
            if (TrackProgressChanged != null)
            {
                _currentTrackPositionArgs.CurrentPosition = position;
                _currentTrackPositionArgs.CurrentPositionString = _wmPlayer.controls.currentPositionString;
                TrackProgressChanged(null, _currentTrackPositionArgs);
            }
        }

        private static void OnTrackChanged()
        {
            if (TrackChanged != null)
            {
                _newTrackArgs.CurrentTrack = _currentPlaylist[_currentTrackIndex];
                TrackChanged(null, _newTrackArgs);
            }
        }

        private static void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            int songPosition = (int)_wmPlayer.controls.currentPosition; 
            OnSongProgressChanged(songPosition);
        }
    }
}