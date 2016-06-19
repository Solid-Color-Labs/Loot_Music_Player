 using System.Collections.Generic;
 using System.IO;
 using System.Linq;
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
    /* To read an write to/from the database, 
     * LINQ to SQL is used.
     */
    static class TrackDb
    {
        public static void AddTracks(ICollection<Track> trackList)
        {
            using (LinqToSqlDataContext dbContext = new LinqToSqlDataContext())
            {
                var tracks = from field in trackList
                             select new TrackTable
                             {
                                 Title = field.Title,
                                 Album = field.Album,
                                 Artist = field.Artist,
                                 Duration = TimeUtil.ConvertTimeToTicks(field.Duration),
                                 Path = field.Path,
                                 Image_Path = field.ImagePath
                             };
                dbContext.TrackTables.InsertAllOnSubmit(tracks);
                dbContext.SubmitChanges();
            }
        }

        public static void RemoveTrack(Track track)
        {
            LinqToSqlDataContext dbContext = new LinqToSqlDataContext();
            dbContext.ExecuteCommand("DELETE FROM Playlist_Detail WHERE Track_Id = {0}", track.Id);
            dbContext.ExecuteCommand("DELETE FROM Track WHERE Track_Id = {0}", track.Id);
            dbContext.SubmitChanges();
            ReseedIfEmpty(dbContext);
        }

        public static void RemoveTrack(int trackId)
        {
            LinqToSqlDataContext dbContext = new LinqToSqlDataContext();
            dbContext.ExecuteCommand("DELETE FROM Playlist_Detail WHERE Track_Id = {0}", trackId);
            dbContext.ExecuteCommand("DELETE FROM Track WHERE Track_Id = {0}", trackId);
            dbContext.SubmitChanges();
            ReseedIfEmpty(dbContext);
        }

        /* When inserting data into the database, the id is auto-incremented.
         * The DBCC command is a SQL Server command, that resets the auto increment value.
         * If all of the rows have been removed from the database, the auto-increment value
         *  should start back at 0. Without the command, the value would just keep incrementing.
         */
        private static void ReseedIfEmpty(LinqToSqlDataContext context)
        {
            int numberOfRows = context.TrackTables.Count();
            bool isEmpty = numberOfRows == 0;
            if (isEmpty)
            {
                context.ExecuteCommand("DBCC CHECKIDENT('Playlist_Detail', RESEED, 1);");
                context.ExecuteCommand("DBCC CHECKIDENT('Track', RESEED, 1);");
                context.SubmitChanges();
            }
        }

        /* TRUNCATE is a SQL Server keyword, that deletes all of the rows at once.
         * Delete goes through each individual row and deletes them one by one.
         * Here TRUNCATE is only used on the playlist_detail table, becaue it has not
         * foreign key constraints. If a table has foerign key constrains, TRUNCATE cannot
         * be used.
         */
        public static void RemoveAllTracks()
        {
            LinqToSqlDataContext dbContext = new LinqToSqlDataContext();
            dbContext.ExecuteCommand("TRUNCATE TABLE Playlist_Detail");
            dbContext.ExecuteCommand("DELETE FROM Track");
            dbContext.ExecuteCommand("DBCC CHECKIDENT('Track', RESEED, 1);");
            dbContext.SubmitChanges();
        }

        public static IEnumerable<Track> GetAllTracks()
        {
            LinqToSqlDataContext dbContext = new LinqToSqlDataContext();
            IQueryable<Track> query = from track in dbContext.TrackTables
                   select new Track()
                   {
                       Id = track.Track_Id,
                       Title = track.Title,
                       Album = track.Album,
                       Artist = track.Artist,
                       Path = track.Path,
                       Duration = TimeUtil.ConvertTicksToTimeSpan(track.Duration),
                       FormattedDuration = TimeUtil.FormatTimeSpan(track.Duration),
                       ImagePath = FileIO.CheckAlbumArtFilepath(track.Image_Path),
                       ImageSource = ImageUtil.GetImageSourceFromPath(track.Image_Path)
                   };
            RemoveIfFileNotExists(query);
            return query.ToList();
        }

        private static void RemoveIfFileNotExists(IEnumerable<Track> tracks)
        {
            foreach (var track in tracks)
            {
                if (!File.Exists(track.Path))
                {
                    RemoveTrack(track.Id);
                }
            }
        }
    }
}