using LightData.Auth.Settings;
using LightData.CMS.Modules.Library;
using LightData.CMS.Modules.Repository;
using LightData.Site;
using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LightDataTable.Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthSettings.GetFileById += (Guid fileId) =>
            {
                using (var rep = new Repository())
                    return rep.Get<FileItem>().Where(x => x.Id == fileId).Execute();
            };
        }
    }
}
