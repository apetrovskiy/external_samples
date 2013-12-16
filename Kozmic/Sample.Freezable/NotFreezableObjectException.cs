namespace Sample.Freezable
{
    using System;

    internal class NotFreezableObjectException : Exception
    {
        public NotFreezableObjectException(object obj)
        {
        }
    }
}