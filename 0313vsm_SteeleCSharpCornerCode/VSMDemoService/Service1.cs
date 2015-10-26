using System;
using System.ServiceProcess;
using Nancy.Hosting.Self;
using VSMDemo.Web;

namespace VSMDemoService
{
	public partial class Service1 : ServiceBase
	{
		private NancyHost nancyHost;

		public Service1()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			nancyHost = new NancyHost(new Uri("http://localhost:9665"), new CustomBootstrapper());
			nancyHost.Start();
		}

		protected override void OnStop()
		{
			nancyHost.Stop();
		}
	}
}
