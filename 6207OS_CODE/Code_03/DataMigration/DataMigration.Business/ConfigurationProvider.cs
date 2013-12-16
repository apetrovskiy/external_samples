using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Ninject.Activation;

namespace DataMigration.Business
{
    public class ConfigurationProvider : Provider<string>
    {
        protected override string CreateInstance(IContext context)
        {
            if (context.Request.Target == null)
            {
                throw new Exception("Target required.");
            }
            var paramName = context.Request.Target.Name;
            string value = ConfigurationManager.AppSettings[paramName];
            if (string.IsNullOrEmpty(value))
            {
                value = ConfigurationManager.ConnectionStrings[paramName].ConnectionString;
            }
            return value;
        }
    }
}
