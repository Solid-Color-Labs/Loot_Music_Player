using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
namespace MusicPlayer.View
{
    public partial class PlaylistView : UserControl
    {
        public PlaylistView()
        {
            InitializeComponent();
        }

        /* In the MVVM pattern, anything UI-related should be in the 
         * code behind the view. When the user clicks a button, this event
         * handles displaying the popup, and showing the textbox in a default state.
         */
        private void AddPlaylistButton_Click(object sender, RoutedEventArgs e)
        {
            PlaylistPopup.IsOpen = true;
            PlaylistTextBox.Text = "New Playlist";
            PlaylistTextBox.Focus();
            PlaylistTextBox.SelectAll();
        }

        private void PlaylistTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PlaylistPopup.IsOpen = false;
            }
        }
    }
}
