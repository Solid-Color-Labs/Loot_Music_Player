using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using File = TagLib.File;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

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
    public static class FileIO
    {
        public static string[] GetFilePathsFromDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Audio Files (.mp3)|*.mp3"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileNames;
            }
            return null;
        }

        /* This method checks if the album art has already been saved to the users computer.
         * If it has then it just returns a filepath to the album art image file.
         * 
         * The reason the album art is saved, is because it's much easier than converting
         * the bytes from an mp3 file, everytime the application loads, to an image file.
         */
        public static string GetAlbumArtPath(File file, string filename)
        {
            string albumArtPath = CreateAlbumArtPath(filename);
            bool hasImage = ImageUtil.HasImage(file);
            if (!System.IO.File.Exists(albumArtPath) && hasImage)
            {
                SaveAlbumArtToDirectory(albumArtPath, file);
            }
            else if (!hasImage)
            {
                albumArtPath = GetDefaultAlbumPath();
                if (!System.IO.File.Exists(albumArtPath))
                {
                   SaveDefaultAlbumArtToDirectory(albumArtPath);
                } 
            }
            return albumArtPath;
        }

        private static string CreateAlbumArtPath(string filename)
        {
            return GetDocumentsDirectory("album_art") + "\\" + filename + ".png";
        }

        /*
        No check is done to see if the directory exists before creating it,
        because if the directory already exists, a DirectoryInfo object is returned,
        and no exception is thrown, as per msdn documentation. 
        */
        private static string GetDocumentsDirectory(string subfolder)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Music_Player";
            DirectoryInfo documentDirectory = Directory.CreateDirectory(documentsPath + "\\" + subfolder);
            return documentDirectory.FullName;
        }

        private static void SaveAlbumArtToDirectory(string path, File file)
        {
            MemoryStream ms = ImageUtil.GetMemoryStreamFromFile(file);
            Image image = Image.FromStream(ms);
            image.Save(path, ImageFormat.Png);
            image.Dispose();
        }

        private static string GetDefaultAlbumPath()
        {
            return GetDocumentsDirectory("album_art") + "\\default_album_art.png";
        }

        /* The default album art is saved to the directory, because the user has
         * read and write access to the directory and could potentialy delete the
         * default album art file.
         */
        private static void SaveDefaultAlbumArtToDirectory(string path)
        {
            Bitmap defaultAlbum = Properties.Resources.default_album_art;
            defaultAlbum.Save(path, ImageFormat.Png);
            defaultAlbum.Dispose();
        }

        public static string CheckAlbumArtFilepath(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                string defaultPath = GetDefaultAlbumPath();
                SaveDefaultAlbumArtToDirectory(path);
                return defaultPath;
            }
            return path;
        }
    }
}