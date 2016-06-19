using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

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
    /* The ViewModelLocater is a default boilerplate class that is
     * used as part of the MVVM light framework. It is declared
     * in the App.xaml.  
     */
    public class ViewModelLocator
    {
        // Boilerplate code for registering ViewModels
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<TrackViewModel>();
            SimpleIoc.Default.Register<HeaderMenuViewModel>();
            SimpleIoc.Default.Register<MusicControlViewModel>();
            SimpleIoc.Default.Register<TrackControlViewModel>();
            SimpleIoc.Default.Register<CurrentlyPlayingViewModel>();
            SimpleIoc.Default.Register<SearchBoxViewModel>();
            SimpleIoc.Default.Register<PlaylistViewModel>();
        }

        /* Properties using expression body to return an instance of the viewmodel when
         * MVVM light requests a ViewModel.
         */
        public static MainWindowViewModel MainWindowViewModel => ServiceLocator.Current.GetInstance<MainWindowViewModel>();
        public static TrackViewModel TrackViewModel => ServiceLocator.Current.GetInstance<TrackViewModel>();
        public static HeaderMenuViewModel HeaderMenuViewModel => ServiceLocator.Current.GetInstance<HeaderMenuViewModel>();
        public static MusicControlViewModel MusicControlViewModel => ServiceLocator.Current.GetInstance<MusicControlViewModel>();
        public static TrackControlViewModel TrackControlViewModel => ServiceLocator.Current.GetInstance<TrackControlViewModel>();
        public static CurrentlyPlayingViewModel CurrentlyPlayingViewModel => ServiceLocator.Current.GetInstance<CurrentlyPlayingViewModel>();
        public static SearchBoxViewModel SearchBoxViewModel => ServiceLocator.Current.GetInstance<SearchBoxViewModel>();
        public static PlaylistViewModel PlaylistViewModel => ServiceLocator.Current.GetInstance<PlaylistViewModel>();
    }
}