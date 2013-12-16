using System;
using Ninject;

namespace Samples.Encryption
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load("typeRegistrations.xml");

            var encryptor = kernel.Get<IEncryptor>();
            Console.WriteLine(encryptor.Encrypt("Hello"));
            Console.ReadKey();
        }
    }
}
