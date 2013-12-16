using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

namespace HelloNinject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var kernel = new Ninject.StandardKernel())
            {
                var service = kernel.Get<SalutationService>();
                service.SayHello();
            }
        }
    }

    class SalutationService
    {
        public void SayHello()
        {
            Console.WriteLine("Hello Ninject!");
        }
    }
}
