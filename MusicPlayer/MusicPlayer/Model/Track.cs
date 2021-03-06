﻿using System;
using System.Windows.Media;

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
namespace MusicPlayer.Model
{
    // This class is a business object/model.
    public class Track 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public TimeSpan Duration { get; set; } 
        public string FormattedDuration { get; set; } 
        public string Path { get; set; }
        public string ImagePath { get; set; } 
        public ImageSource ImageSource { get; set; } 
    }
}