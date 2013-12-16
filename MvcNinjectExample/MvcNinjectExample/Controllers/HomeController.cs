using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcNinjectExample.Controllers
{
    public class HomeController : Controller
    {
		MvcNinjectExample.Logging.ILogger _logger;

		public HomeController(MvcNinjectExample.Logging.ILogger logger)
		{
			_logger = logger;
		}

        public ActionResult Index()
		{
			_logger.LogMessage("Running index page!");

			return Content("Message logged");
        }
    }
}
