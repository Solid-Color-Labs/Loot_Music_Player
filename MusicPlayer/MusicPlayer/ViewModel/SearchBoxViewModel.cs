using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MusicPlayer.Collections;
using MusicPlayer.Extensions;
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
Date: 4/25/16
*/
namespace MusicPlayer.ViewModel
{
    public class SearchBoxViewModel : ViewModelBase
    {
        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged();
                ObservableTracks.Refresh();
                _isNewDataset = true;
            }
        }

        private ICollectionView _observableTracks;
        public ICollectionView ObservableTracks
        {
            get { return _observableTracks; }
            private set
            {
                _observableTracks = value;
                RaisePropertyChanged();
            }
        }

        /* The current position is synchronized with the selected index
         * of the ListView bound to the ObservableTracksProperty,
         * via the ListView's IsSynchronizedWithCurrentItem="True" property.
         */
        private int SelectedTrackIndex => ObservableTracks.CurrentPosition;

        private bool _isNewDataset;

        public RelayCommand<int> PlayTrackCommand { get; private set; }
        public RelayCommand UpdateCollectionViewCommand { get; private set; }

        public SearchBoxViewModel()
        {
            InitCommands();
            InitObservable();
        }

        private void InitCommands()
        {
            PlayTrackCommand = new RelayCommand<int>(PlayTrack, CanExecute);
            UpdateCollectionViewCommand = new RelayCommand(UpdateCollectionView);
        }

        /* ObservableTracks is initialized to an empty collection to
         * avoid a NullReferenceException, because the ObservableTracks.Refresh()
         * is located in the SearchText property setter, which is called before
         * an actual collection has been assigned to ObservableTracks.
         */
        private void InitObservable()
        {
            ObservableTracks = CollectionViewSource.GetDefaultView(Enumerable.Empty<Track>());
        }

        /* This method updates the collection view,
         * to ensure that if the user added 
         * items to the source, they show 
         * up in the CollectionView.
         */
        private void UpdateCollectionView()
        {
            ObservableTracks = new CollectionViewSource { Source = TrackCollection.Tracks }.View; 
            ObservableTracks.Filter = TrackCollectionViewFilter;
        }

        /* This method is a filter, that is 
         * attached to the ObservableTracks CollectionView.
         */
        private bool TrackCollectionViewFilter(object item)
        {
            Track track = item as Track;
            if (track != null && !string.IsNullOrEmpty(SearchText))
            {
                return track.Title.ContainsIgnoreCase(SearchText)
                       || track.Artist.ContainsIgnoreCase(SearchText)
                       || track.Album.ContainsIgnoreCase(SearchText);
            }
            return false;
        }

        private void PlayTrack(int selectedItemsCount)
        {
            SetMediaPlayerPlaylist();
            MediaPlayer.Play(SelectedTrackIndex);
        }

        /* A new List is created and initialized via a shallow copy, because
         * the CollectionView returns a list of filtered items in the form of 
         * a Enumerable interface, and the SetPlaylist method takes a 
         * collection, which implements the IList interfave.
         */
        private void SetMediaPlayerPlaylist()
        {
            if (_isNewDataset)
            {
                MediaPlayer.SetPlaylist(new List<Track>(GetFilteredTracks()));
                _isNewDataset = false;
            }
        }

        private IEnumerable<Track> GetFilteredTracks()
        {
            return ObservableTracks.Cast<Track>();
        }

        public bool CanExecute(int selectedItemsCount)
        {            
            return selectedItemsCount == 1; 
        }
    }
}