﻿// Patterns: 1
// Matches: Repository.cs
// NotMatches: Foo.cs

using SimpleInjector;
using SimpleInjector.Extensions;
using TestApplication.Types;

namespace TestApplication.SimpleInjector
{
    public class RegisterOpenGenericLifestyleNonGeneric
    {
        public RegisterOpenGenericLifestyleNonGeneric()
        {
            var container = new Container();

            container.RegisterOpenGeneric(typeof(IRepository<>), typeof(Repository<,>), Lifestyle.Transient);
        } 
    }
}