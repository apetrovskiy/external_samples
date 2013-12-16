using System;

namespace Samples.ServiceLocator
{
    class Manual
    {
        public static T Locate<T>(params object[] args)
        {
            return (T)Activator.CreateInstance(typeof(T), args);
        }
    }
}