using System;

namespace Samples.MailService.SingletonApproach
{
    class ConsoleLogger : ILogger
    {
        public static ConsoleLogger Instance = new ConsoleLogger();

        static ConsoleLogger()
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit.
            // More info: http://www.yoda.arachsys.com/csharp/singleton.html
        }

        private ConsoleLogger()
        {
            // Hiding constructor
        }

        public void Log(string message)
        {
            Console.WriteLine("{0}: {1}", DateTime.Now, message);
        }
    }
}