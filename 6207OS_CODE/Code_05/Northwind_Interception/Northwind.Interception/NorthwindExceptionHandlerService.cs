using System;

namespace Northwind.Interception
{
    public class NorthwindExceptionHandlerService : IExceptionHandlerService
    {
        public void HandleException(Exception exception)
        {
            throw new NorthwindException("Exception occurred!", exception);
        }
    }
}