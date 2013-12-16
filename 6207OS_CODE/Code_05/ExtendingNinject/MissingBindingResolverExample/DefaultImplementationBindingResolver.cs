using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Activation;
using Ninject.Activation.Providers;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Planning.Bindings;
using Ninject.Planning.Bindings.Resolvers;

namespace MissingBindingResolverExample
{
    public class DefaultImplementationBindingResolver : NinjectComponent, IMissingBindingResolver
    {

        public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, IRequest request)
        {

            var service = request.Service;
            if (!service.IsInterface || !service.Name.StartsWith("I"))
            {
                return Enumerable.Empty<IBinding>();
            }
            return new[]
                    {
                        new Binding(service)
                            {
                                ProviderCallback =
                                    StandardProvider.GetCreationCallback(GetDefaultImplementationType(service))
                            }
                    };
        }

        private Type GetDefaultImplementationType(Type service)
        {
            var typeName = string.Format("{0}.{1}", service.Namespace,
                                            service.Name.TrimStart('I'));
            return Type.GetType(typeName);
        }
    }
}