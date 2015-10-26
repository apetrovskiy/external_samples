// Patterns: 1
// Matches: Foo.cs,Bar.cs,Baz.cs,FooBar.cs
// NotMatches: IFoo.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromAssemblyContainingPick : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.FromAssemblyContaining<IFoo>().Pick(),
                Classes.FromAssemblyContaining<IFoo>().Pick(),
                Castle.MicroKernel.Registration.Types.FromAssemblyContaining<IFoo>().Pick()
                );
        }
    }
}