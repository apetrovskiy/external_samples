using MusicBox.Framework.Common;
using MusicBox.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicBox.Web.Helpers
{
    public static class ArtistHelper
    {
        public static Artist SetupArtist(musicBoxDBEntities db, MusicBox.Framework.Musicbrainz.Model.Artist artist, MusicBox.Framework.Musicbrainz.Model.Collections.ReleaseList releases)
        {
            var dbArtist = db.Artists.Add(new Artist() { MusicBrainzID = artist.Id, ArtistName = artist.Name, ArtistDescription = "" });

            foreach (var release in releases)
            {
                var album = db.Releases.Add(new Release() { ArtistID = dbArtist.ArtistID, Artist = dbArtist, MusicBrainzID = release.Id, ReleaseDate = release.Date, ReleaseName = release.Title, Status = Constants.Status.SKIPPED });

                if (release.MediumList.Count > 0)
                {
                    var medium = release.MediumList[0];

                    if (medium.Tracks.Count() > 0)
                    {
                        foreach(var mbTrack in medium.Tracks)
                        {
                            var track = db.Tracks.Add(new Track() { MusicBrainzID = mbTrack.Id, Release = album, Status = Constants.Status.SKIPPED, Position = mbTrack.Position, TrackName = mbTrack.Recordring.Title, MusicBrainzRecordingID = mbTrack.Recordring.Id, Length = mbTrack.Recordring.Length, Path = "" });
                        }
                    }
                }
            }

            db.SaveChanges();

            return dbArtist;
        }
    }
}