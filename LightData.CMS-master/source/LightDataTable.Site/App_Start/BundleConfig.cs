using LightData.CMS.Modules.Helper;
using System.Web.Optimization;
using System.Linq;

namespace LightData.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
#if DEBUG
            BundleTable.EnableOptimizations = false;
#else
            BundleTable.EnableOptimizations = true;
#endif
            //var files = Methods.GetTheme();

            var style = new StyleBundle("~/bundles/css")
                .Include("~/Content/Styles/Layout.css");

            //foreach (var css in files.Where(x => x.ToLower().EndsWith("css")))
            //{
            //    var tCss= css.Replace(@"\", "/");
            //    style.Include(string.Format("~/{0}", tCss));
            //}


            var scripts = new ScriptBundle("~/bundles/jquery")
                .Include("~/Content/Script/jquery-3.2.1.min.js");

            //foreach (var script in files.Where(x => x.ToLower().EndsWith("js")))
            //{
            //    var js = script.Replace(@"\", "/");
            //    style.Include(string.Format("~/{0}", js));
            //}

            bundles.Add(style);
            bundles.Add(scripts);
        }
    }
}