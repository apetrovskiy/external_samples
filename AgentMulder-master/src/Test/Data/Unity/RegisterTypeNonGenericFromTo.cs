﻿// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Microsoft.Practices.Unity;
using TestApplication.Types;

namespace TestApplication.Unity
{
    public class RegisterTypeNonGenericFromTo
    {
        public RegisterTypeNonGenericFromTo()
        {
            var container = new UnityContainer();
            container.RegisterType(typeof(ICommon), typeof(CommonImpl1));
        }
    }
}