namespace Sample.Freezable
{
    using System;
    using Castle.DynamicProxy; //using Castle.Core.Interceptor;

    [Serializable]
    public class FreezableInterceptor : IInterceptor, IFreezable
    {
        private bool _isFrozen;

        #region IFreezable Members

        public void Freeze()
        {
            _isFrozen = true;
        }

        public bool IsFrozen
        {
            get { return _isFrozen; }
        }

        #endregion

        #region IInterceptor Members

        public void Intercept(IInvocation invocation)
        {
            if (_isFrozen && invocation.Method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase))
            {
                throw new ObjectFrozenException();
            }

            invocation.Proceed();
        }

        #endregion
    }
}