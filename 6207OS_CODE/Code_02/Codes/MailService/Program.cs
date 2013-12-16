using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace Samples.MailService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var kernel = new StandardKernel())
            {
                kernel.Bind<ILogger>().To<ConsoleLogger>();
                var mailService = kernel.Get<MailService>();
                mailService.SendMail("someone@domain.com", "Hi", null);
            }
        }
    }
}