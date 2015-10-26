using System;
using System.IO;
using Tools;

namespace ImpersonatorDemo
{
	/// <summary>
	/// Main class for the demo application.
	/// Call this application with a low privileg account to test.
	/// </summary>
	class Program
	{
		/// <summary>
		/// The main entry point.
		/// </summary>
		[STAThread]
		static void Main( string[] args )
		{
			// Impersonate, automatically release the impersonation.
			using ( new Impersonator( "yourUsername", "yourDomain", "yourPassword" ) )
			{
				// The following code is executed under the impersonated user.
				string[] files = Directory.GetFiles( "c:\\windows" );
			}
		}
	}
}