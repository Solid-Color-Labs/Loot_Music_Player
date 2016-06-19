using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TagLib;
using File = TagLib.File;

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
namespace MusicPlayer.Utils
{
    public static class ImageUtil
    {
        public static ImageSource GetImageSourceFromPath(string path)
        {
            BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            return bitmap;
        }

        public static bool HasImage(File file)
        {
            IPicture[] image = file.Tag.Pictures;
            return image != null && image.Length != 0;
        }

        /* An image is stored on an mp3 file as bytes.
         * Tabglib-Sharp allows for the retrieval of those bytes.
         * This method converts the bytes to a MemoryStream.
         */
        public static MemoryStream GetMemoryStreamFromFile(File file)
        {
            Tag tag = file.Tag;
            IPicture picture = tag.Pictures[0];
            ByteVector vector = picture.Data;
            byte[] bytes = vector.Data;
            return new MemoryStream(bytes);
        }
    }
}