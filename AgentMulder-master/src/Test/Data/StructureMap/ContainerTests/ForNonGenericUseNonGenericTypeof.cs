﻿// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForNonGenericUseNonGenericTypeOf
    {
        public ForNonGenericUseNonGenericTypeOf()
        {
            var container = new Container(x => x.For(typeof(IFoo)).Use(typeof(Foo)));
        } 
    }
}