using DataMigration.Business;
using DataMigration.SqlDataAccess;
using DataMigration.XmlDataAccess;
using Ninject.Modules;

namespace DataMigration.Console
{
    internal class CompositionModule : NinjectModule
    {
        public override void Load()
        {
            Bind<string>().ToProvider<ConfigurationProvider>()
                .WhenTargetHas<ConfigurationAttribute>();

            Bind<IShippersRepository>().To<ShippersSqlRepository>()
                .When(r => r.Target.Name.StartsWith("source"));

            Bind<IShippersRepository>().To<ShippersXmlRepository>()
                .When(r => r.Target.Name.StartsWith("target"));

            //Bind<IShippersRepository>().To<ShippersSqlRepository>()
            //    .When(r => r.Target.Name.StartsWith("source"))
            //    .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["NorthwindConnectionString"]);

            //Bind<IShippersRepository>().To<ShippersXmlRepository>()
            //        .When(r => r.Target.Name.StartsWith("target"))
            //    .WithConstructorArgument("XmlRepositoryPath", ConfigurationManager.AppSettings["XmlRepositoryPath"]);

            //Bind<IShippersRepository>().To<ShippersSqlRepository>()
            //    .WhenTargetHas<SourceAttribute>()
            //    .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["NorthwindConnectionString"]);

            //Bind<IShippersRepository>().To<ShippersXmlRepository>()
            //    .WhenTargetHas<TargetAttribute>()
            //    .WithConstructorArgument("XmlRepositoryPath", ConfigurationManager.AppSettings["XmlRepositoryPath"]);

            //Bind<IShippersRepository>().To<ShippersSqlRepository>()
            //    .WithMetadata("IsSource", true)
            //    .WithConstructorArgument("connectionString", ConfigurationManager.ConnectionStrings["NorthwindConnectionString"]);

            //Bind<IShippersRepository>().To<ShippersXmlRepository>()
            //    .WithMetadata("IsSource", false)
            //    .WithConstructorArgument("XmlRepositoryPath", ConfigurationManager.AppSettings["XmlRepositoryPath"]);

        }


    }
}