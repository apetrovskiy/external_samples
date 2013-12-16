using System;
using System.Configuration;
using System.Xml.Linq;
using DataMigration.Business;
using Ninject;
using Ninject.Planning.Bindings;

namespace DataMigration.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new CompositionModule());
            var shippersService = kernel.Get<ShippersService>();
            shippersService.MigrateShippers();

        }
    }
}
