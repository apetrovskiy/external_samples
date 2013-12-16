using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcNinjectExample.Logging
{
	public interface ILogger
	{
		void LogMessage(string message);
	}
}