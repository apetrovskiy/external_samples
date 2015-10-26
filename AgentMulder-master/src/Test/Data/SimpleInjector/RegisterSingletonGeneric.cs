﻿// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using SimpleInjector;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterSingletonGeneric
    {
        public RegisterSingletonGeneric()
        {
            var container = new Container();

            container.RegisterSingle<ICommon, CommonImpl1>();
        } 
    }
}