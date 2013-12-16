using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using Ninject.Planning.Bindings.Resolvers;

namespace MissingBindingResolverExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Components.Add<IMissingBindingResolver, DefaultImplementationBindingResolver>();
            kernel.Get<IWriter>().Write();
        }
    }
}
