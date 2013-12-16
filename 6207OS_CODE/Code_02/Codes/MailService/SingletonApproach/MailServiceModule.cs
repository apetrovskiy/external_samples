using Ninject.Modules;
using Ninject.Web.Common;

namespace Samples.MailService.SingletonApproach
{
    class MailServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILogger>().To<ConsoleLogger>().InSingletonScope();
            Bind<MailServerConfig>().ToSelf().InRequestScope();
        }
    }
}