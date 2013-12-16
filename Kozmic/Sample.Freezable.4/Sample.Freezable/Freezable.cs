namespace Sample.Freezable
{
    using System.Linq;
    using Castle.DynamicProxy;

    public static class Freezable
    {

        private static readonly ProxyGenerator _generator = new ProxyGenerator(new PersistentProxyBuilder());

        public static bool IsFreezable(object obj)
        {
            return AsFreezable(obj) != null;
        }

        private static IFreezable AsFreezable(object target)
        {
            if (target == null)
            {
                return null;
            }
            var hack = target as IProxyTargetAccessor;
            if (hack == null)
            {
                return null;
            }
            return hack.GetInterceptors().FirstOrDefault(i => i is FreezableInterceptor) as IFreezable;
        }

        public static bool IsFrozen(object obj)
        {
            IFreezable freezable = AsFreezable(obj);
            return freezable != null && freezable.IsFrozen;
        }


        public static void Freeze(object freezable)
        {
            IFreezable interceptor = AsFreezable(freezable);
            if (interceptor == null)
            {
                throw new NotFreezableObjectException(freezable);
            }
            interceptor.Freeze();
        }


        public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class, new()
        {
            var freezableInterceptor = new FreezableInterceptor();
            var options = new ProxyGenerationOptions(new FreezableProxyGenerationHook());
            var proxy = _generator.CreateClassProxy(typeof(TFreezable), options, new CallLoggingInterceptor(), freezableInterceptor);
            return proxy as TFreezable;
        }
    }
}