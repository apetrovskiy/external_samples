namespace Sample.Freezable
{
    using System.Collections.Generic;
    using Castle.DynamicProxy;

    public static class Freezable
    {
        private static readonly IDictionary<object, IFreezable> _freezables = new Dictionary<object, IFreezable>();

        private static readonly ProxyGenerator _generator = new ProxyGenerator(new PersistentProxyBuilder());

        public static bool IsFreezable(object obj)
        {
            return obj != null && _freezables.ContainsKey(obj);
        }


        public static void Freeze(object freezable)
        {
            if (!IsFreezable(freezable))
            {
                throw new NotFreezableObjectException(freezable);
            }
            _freezables[freezable].Freeze();
        }

        public static bool IsFrozen(object freezable)
        {
            return IsFreezable(freezable) && _freezables[freezable].IsFrozen;
        }

        public static TFreezable MakeFreezable<TFreezable>() where TFreezable : class, new()
        {
            var freezableInterceptor = new FreezableInterceptor();
            var options = new ProxyGenerationOptions(new FreezableProxyGenerationHook());
            var proxy = _generator.CreateClassProxy(typeof(TFreezable), options, new CallLoggingInterceptor(), freezableInterceptor);
            _freezables.Add(proxy, freezableInterceptor);
            return proxy as TFreezable;
        }
    }
}