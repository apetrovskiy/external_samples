﻿// Patterns: 1
// Matches: Foo.cs,Baz.cs
// NotMatches: Bar.cs

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using TestApplication.Types;

namespace TestApplication.Windsor.TestCases
{
    public class FromTypesParamsBasedOnGeneric : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.From(typeof(Foo), typeof(Bar), typeof(Baz)).BasedOn<IFoo>(),
                Classes.From(typeof(Foo), typeof(Bar), typeof(Baz)).BasedOn<IFoo>(),
                Castle.MicroKernel.Registration.Types.From(typeof(Foo), typeof(Bar), typeof(Baz)).BasedOn<IFoo>()
                );
        }
    }
}