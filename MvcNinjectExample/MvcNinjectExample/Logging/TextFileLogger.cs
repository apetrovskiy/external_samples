using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Hosting;

namespace MvcNinjectExample.Logging
{
	public class TextFileLogger : ILogger
	{
		public void LogMessage(string message)
		{
			string path = Path.Combine(HostingEnvironment.MapPath("~/app_data"), "log.txt");
			using (FileStream stream = new FileStream(path, FileMode.Append))
			{
				StreamWriter writer = new StreamWriter(stream);
				writer.WriteLine(message);
				writer.Flush();
			}
		}
	}
}