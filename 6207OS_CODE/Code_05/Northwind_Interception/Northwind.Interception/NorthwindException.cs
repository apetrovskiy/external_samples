using System;

namespace Northwind.Interception
{
    public class NorthwindException : Exception
    {
        public NorthwindException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}