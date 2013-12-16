using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using log4net.Config;
using InjectAttribute = Northwind.Core.InjectAttribute;

namespace Northwind.Mvc
{

    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        public static void RegisterLog4Net()
        {
            var log4NetConfigFile = ConfigurationManager.AppSettings["log4netConfigFile"];
            var log4NetConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, log4NetConfigFile);
            XmlConfigurator.Configure(new FileInfo(log4NetConfigPath));
        }

        protected override void OnApplicationStarted()
        {
            RegisterLog4Net();
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected override IKernel CreateKernel()
        {
            var ninjectSettings = new NinjectSettings
                                        {
                                            InjectAttribute = typeof(InjectAttribute)
                                        };
            var kernel = new StandardKernel(ninjectSettings);

            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }



    }
}