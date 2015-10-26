using System.Reflection;
using System.Runtime.CompilerServices;
using log4net.Config;

[assembly: AssemblyTitle("AllureCSharpCommons")]
[assembly: AssemblyDescription("C-Sharp implementations of core Allure events, exceptions, lifecycle entry-points.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Ilya Murzinov")]
[assembly: AssemblyProduct("")]
[assembly: AssemblyCopyright("Ilya Murzinov, murz42@gmail.com")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("0.1.0.*")]
// The following attributes are used to specify the signing key for the assembly,
// if desired. See the Mono documentation for more information about signing.
//[assembly: AssemblyDelaySign(false)]
//[assembly: AssemblyKeyFile("")]

[assembly: InternalsVisibleTo("AllureCSharpCommons.Tests")]
[assembly: XmlConfigurator(Watch = true)]