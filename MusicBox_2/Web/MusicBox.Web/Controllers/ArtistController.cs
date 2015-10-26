using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicBox.Web.Data;
using MusicBox.Web.Models;
using MusicBox.Framework.Common;
using MusicBox.Web.Helpers;

namespace MusicBox.Web.Controllers
{
    [Authorize]
    public class ArtistController : Controller
    {
        private musicBoxDBEntities db = new musicBoxDBEntities();

        // GET: /Artist/
        public ActionResult Index()
        {
            return View(db.Artists.ToList());
        }

        [HttpGet]
        public ActionResult Search(SearchModel model)
        {
            var found = MusicBox.Framework.Musicbrainz.Model.Artist.Search(model.SearchTerm);

            List<SearchResultModel> results = new List<SearchResultModel>();

            foreach (var result in found)
            {
                results.Add(new SearchResultModel() { Name = result.Name, MBID = result.Id, CoverArtURL = "" });
            }

            return View(results);
        }

        public ActionResult AddArtist(string mbid)
        {
            var mbArtist = new MusicBox.Framework.Musicbrainz.Model.Artist(); 
            var releases = MusicBox.Framework.Musicbrainz.Discography.BuildDiscography(mbid, out mbArtist);

            var artist = ArtistHelper.SetupArtist(db, mbArtist, releases);

            return RedirectToAction("ViewArtist", artist.ArtistID);
        }

        public ActionResult ViewArtist(int id)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
