using System;
using System.Linq;
using System.Reflection;
using Castle.Core.Interceptor;
using Castle.DynamicProxy;

namespace Sample.Freezable
{
    public class FreezableInterceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            if (IsSetter(method))
                return interceptors;
            return interceptors.Where( i => !( i is FreezableInterceptor ) ).ToArray();
        }

        private bool IsSetter( MethodInfo method )
        {
            return method.IsSpecialName && method.Name.StartsWith( "set_", StringComparison.Ordinal );
        }
    }
}