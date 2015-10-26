using MusicBox.Web.Data;
using MusicBox.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicBox.Web.Controllers
{
    public class SettingsController : Controller
    {
        musicBoxDBEntities db = new musicBoxDBEntities();
        //
        // GET: /Settings/
        public ActionResult Index()
        {
            var settings = SettingsHelper.BuildSettingsFile(db);

            return View(settings);
        }
	}
}