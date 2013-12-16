namespace Sample.Freezable
{
    using System;
    using Castle.Core.Interceptor;

    [Serializable]
    public class FreezableInterceptor : IInterceptor, IFreezable,IHasCount
    {
        private bool _isFrozen;
        private int _count;

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
            _count++;
            if (_isFrozen && invocation.Method.Name.StartsWith("set_", StringComparison.OrdinalIgnoreCase))
            {
                throw new ObjectFrozenException();
            }

            invocation.Proceed();
        }

        #endregion

        public int Count
        {
            get
            {
                return _count;
            }
        }
    }
}