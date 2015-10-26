using Nancy;

namespace VSMDemo.Web
{
	public class MainModule : NancyModule
	{
		public MainModule()
		{
			Get["/"] = x => View["index.html"];
		}
	}
}
