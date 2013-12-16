using System;

namespace MissingBindingResolverExample
{
    public class Writer : IWriter
    {
        public void Write()
        {
            Console.WriteLine("Hello");
        }
    }
}