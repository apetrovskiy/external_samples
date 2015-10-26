// Patterns: 1
// Matches: CommonImpl123.cs
// NotMatches: Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.ComponentTestCases
{
    public class ComponentFor3GenericImplementedByGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ICommon, ICommon2, ICommon3>().ImplementedBy<CommonImpl123>());
        }
    }
}