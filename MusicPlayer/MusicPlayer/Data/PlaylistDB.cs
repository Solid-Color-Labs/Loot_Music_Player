using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
Date: 4/27/16
*/
namespace MusicPlayer.Data
{
    public class PlaylistDb
    {
        public static void AddPlaylist(Playlist playlist)
        {
            using (LinqToSqlDataContext dbContext = new LinqToSqlDataContext())
            {
                PlaylistTable playlistTable = new PlaylistTable()
                {
                    Title = playlist.Title
                };
                dbContext.PlaylistTables.InsertOnSubmit(playlistTable);
                dbContext.SubmitChanges();
            }
        }

        public static void AddTrackToPlaylist(Playlist playlist, Track track)
        {
            using (LinqToSqlDataContext dbContext = new LinqToSqlDataContext())
            {
                PlaylistDetailTable playlistDetail = new PlaylistDetailTable
                {
                    Playlist_Id = playlist.Id,
                    Track_Id = track.Id
                };
                dbContext.PlaylistDetailTables.InsertOnSubmit(playlistDetail);
                dbContext.SubmitChanges();
            }
        }

        public static void DeleteTrackFromPlaylist(Playlist playlist, Track track)
        {
            using (LinqToSqlDataContext dbContext = new LinqToSqlDataContext())
            {
                dbContext.ExecuteCommand("DELETE FROM Playlist_Detail WHERE Playlist_Id = {0} AND Track_Id = {1}", playlist.Id, track.Id);
                dbContext.SubmitChanges();
                ReseedDetailIfEmpty(dbContext);
            }
        }

        /* This method serves the same purpose as the
         * ReseedIfEmpty method from the TrackDB class.
         * The reason for using this method can be found in
         * the TrackDB ReseedIfEmpty method.
         */
        private static void ReseedDetailIfEmpty(LinqToSqlDataContext context)
        {
            int numberOfRows = context.PlaylistDetailTables.Count();
            bool isEmpty = numberOfRows == 0;
            if (isEmpty)
            {
                context.ExecuteCommand("DBCC CHECKIDENT('Playlist_Detail', RESEED, 1);");
                context.SubmitChanges();
            }
        }

        public static void DeletePlaylist(Playlist playlist)
        {
            using (LinqToSqlDataContext dbContext = new LinqToSqlDataContext())
            {
                dbContext.ExecuteCommand("DELETE FROM Playlist_Detail WHERE Playlist_Id = {0}", playlist.Id);
                dbContext.ExecuteCommand("DELETE FROM Playlist WHERE Playlist_Id = {0}", playlist.Id);
                dbContext.SubmitChanges();
                ReseedIfEmpty(dbContext);
            }
        }

        private static void ReseedIfEmpty(LinqToSqlDataContext context)
        {
            int numberOfRows = context.PlaylistTables.Count();
            bool isEmpty = numberOfRows == 0;
            if (isEmpty)
            {
                context.ExecuteCommand("DBCC CHECKIDENT('Playlist_Detail', RESEED, 1);");
                context.ExecuteCommand("DBCC CHECKIDENT('Playlist', RESEED, 1);");
                context.SubmitChanges();
            }
        }

        /* A sub-query is used here to populate 
         * the track collection in the playlist object. 
         * I felt that since the sub-query is tied to a playlist, 
         * the track objects can be created in this method.
         */
        public static IEnumerable<Playlist> GetAllPlaylists()
        {
            LinqToSqlDataContext dbContext = new LinqToSqlDataContext();
            IQueryable<Playlist> query = from p in dbContext.PlaylistTables
                         select new Playlist
                         {
                             Id = p.Playlist_Id,
                             Title = p.Title,
                             Tracks = new ObservableCollection<Track>(from t in dbContext.TrackTables
                                                                      from pd in t.PlaylistDetailTables
                                      where p.Playlist_Id == pd.Playlist_Id
                                      select new Track
                                      {
                                          Id = t.Track_Id,
                                          Title = t.Title,
                                          Album = t.Album,
                                          Artist = t.Artist,
                                          Duration = TimeUtil.ConvertTicksToTimeSpan(t.Duration),
                                          FormattedDuration = TimeUtil.FormatTimeSpan(t.Duration),
                                          Path = t.Path,
                                          ImagePath = t.Image_Path,
                                          ImageSource = ImageUtil.GetImageSourceFromPath(t.Image_Path)
                                      })
                         };
            return query.ToList();
        }

        public static List<string> GetAllPlaylistNames()
        {
            LinqToSqlDataContext dbContext = new LinqToSqlDataContext();

            IQueryable<string> query = from p in dbContext.PlaylistTables
                                       select p.Title;

            return query.ToList();
        }
    }
}