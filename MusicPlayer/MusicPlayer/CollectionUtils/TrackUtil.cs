using System.Linq;
using MusicPlayer.Collections;
using MusicPlayer.Model;
using MusicPlayer.Utils;
using TagLib;

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
namespace MusicPlayer.CollectionUtils
{
    public static class TrackUtil
    {
        private static int TrackId { get; set; }
        static TrackUtil()
        {
            TrackId = GetMaxId();
        }

        /* This method serves the same purpose as
         * the GetMaxId method in the PlaylistUtil class.
         * The reason for using this method, can be found in the
         * PlaylistUtil class.
         */
        private static int GetMaxId()
        {
            return !TrackCollection.HasTracks() ? 1 : TrackCollection.Tracks.Max(t => t.Id);
        }

        /* If a track doesn't have certain properties, they are assigned 
         * as unknown.
         * 
         * All of the track data is retrieved from an mp3 file via 
         * a library called TagLib-Sharp (https://github.com/mono/taglib-sharp). 
         * The library makes it possible to get the metadata stored on an mp3 file.
         */
        public static Track CreateTrack(string path)
        {
            File file = File.Create(path);
            Tag tag = file.Tag;
            TrackId++;
            Track track = new Track
            {
                Id = TrackId,
                Title = string.IsNullOrEmpty(tag.Title) ? "Unknown" : tag.Title,
                Album = string.IsNullOrEmpty(tag.Album) ? "Unknown" : tag.Album,
                Artist = string.IsNullOrEmpty(tag.FirstPerformer) ? "Unknown" : tag.FirstPerformer,
                Duration = file.Properties.Duration,
                FormattedDuration = TimeUtil.FormatTimeSpan(file.Properties.Duration),
                Path = path
            };
            string imagePath = FileIO.GetAlbumArtPath(file, track.Title);
            track.ImagePath = imagePath;
            track.ImageSource = ImageUtil.GetImageSourceFromPath(imagePath);
            return track;
        }
    }
}