using MusicBox.Framework.Web;
using System;

namespace MusicBox.Debug
{
    class Program
    {
        static void Main(string[] args)
        {
            WebHost hostingClient = new WebHost(true);
            hostingClient.Start();

            Console.WriteLine("");
            Console.WriteLine("To stop service, close the window");

            while(true)
            {

            }
        }
    }
}
