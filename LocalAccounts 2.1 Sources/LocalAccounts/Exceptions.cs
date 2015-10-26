using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LocalAccounts
{
    public class NetworkPathNotFoundException : Exception
    {
        public NetworkPathNotFoundException(string message) : base(message) { }

        public NetworkPathNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class LogonFailureException : Exception
    {
        public LogonFailureException(string message) : base(message) { }

        public LogonFailureException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string message) : base(message) { }

        public AccessDeniedException(string message, Exception innerException) : base(message, innerException) { }
    }
}
