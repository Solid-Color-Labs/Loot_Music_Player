using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
namespace MusicPlayer.View
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        private void DropDownButton_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).ContextMenu.IsEnabled = true;
            ((Button)sender).ContextMenu.PlacementTarget = (Button)sender;
            ((Button)sender).ContextMenu.Placement = PlacementMode.Bottom;
            ((Button)sender).ContextMenu.IsOpen = true;
        }
    }
}
