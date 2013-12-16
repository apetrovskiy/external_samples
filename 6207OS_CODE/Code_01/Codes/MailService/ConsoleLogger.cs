using System;

namespace Samples.MailService
{
    class ConsoleLogger:ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("{0}: {1}", DateTime.Now, message);
        }
    }
}