using System;
using Ninject.Extensions.Interception;

namespace Northwind.Interception
{
    public class ExceptionInterceptor : IInterceptor
    {
        private readonly IExceptionHandlerService exceptionHandlerService;

        public ExceptionInterceptor(IExceptionHandlerService handlerService)
        {
            if (handlerService == null)
            {
                throw new ArgumentNullException("handlerService");
            }
            this.exceptionHandlerService = handlerService;
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                exceptionHandlerService.HandleException(exception);
            }
        }
    }
}