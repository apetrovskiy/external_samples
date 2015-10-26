using System.ServiceProcess;

namespace VSMDemoService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			ServiceBase[] ServicesToRun = new ServiceBase[]
			                              	{
			                              		new Service1()
			                              	};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
