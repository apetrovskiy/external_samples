using System.Web.Mvc;
using LightData.Auth.Controllers;


namespace LightData.CMS.Controllers
{
    public class HomeController : SharedController
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}