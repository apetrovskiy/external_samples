using Ninject;

namespace Samples.MailService.SingletonApproach
{
    internal class Program
    {
        //private static void Main(string[] args)
        //{
        //    var kernel = new StandardKernel();

        //    //kernel.Bind<ILogger>().ToConstant(ConsoleLogger.Instance);
        //    kernel.Bind<ILogger>().To<ConsoleLogger>().InSingletonScope();
        //    // kernel.Bind<MailServerConfig>().ToSelf().InSingletonScope();
        //    kernel.Bind<MailServerConfig>().ToSelf().InRequestScope();
        //    var mailService = kernel.Get<MailService>();

        //    mailService.SendMail("someone@somewhere.com", "Hello Ninject!", null);
        //}

        private static void Main(string[] args)
        {
            using (var kernel = new StandardKernel(new MailServiceModule()))
            {
                var mailService = kernel.Get<MailService>();
                mailService.SendMail("someone@somewhere.com", "Hello", null);
            }
        }
    }
}