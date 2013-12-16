using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samples.MailService
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var mailService = new MailService(new EventLogger());
            mailService.SendMail("someone@somewhere.com", "My first DI App", "Hello World!");
        }
    }
}