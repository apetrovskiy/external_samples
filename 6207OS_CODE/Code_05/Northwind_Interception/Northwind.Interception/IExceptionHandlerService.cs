using System;

namespace Northwind.Interception
{
    public interface IExceptionHandlerService
    {
       void HandleException(Exception exception);
    }
}