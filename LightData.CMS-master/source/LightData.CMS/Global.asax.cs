using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LightData.Auth.Settings;
using LightData.CMS.Modules.Library;
using LightData.CMS.Modules.Repository;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;

namespace LightData.CMS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            HttpConfiguration config = GlobalConfiguration.Configuration;
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            //return JsonConvert.SerializeObject(data, Formatting.Indented, camelCaseFormatter);

            config.Formatters.JsonFormatter.SerializerSettings = camelCaseFormatter;

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthSettings.GetFileById += (Guid fileId) =>
            {
                using (var rep = new Repository())
                    return rep.Get<FileItem>().Where(x => x.Id == fileId).Execute();
            };

            AuthSettings.OnGetUser += (username, password) =>
            {
                using (var rep = new Repository())
                    return rep.Get<User>().Where(x => x.UserName == username && x.Password == password)
                        .LoadChildren(x => x.Role).Execute().Cast<dynamic>().ToList();
            };
      
        }
    }
}
