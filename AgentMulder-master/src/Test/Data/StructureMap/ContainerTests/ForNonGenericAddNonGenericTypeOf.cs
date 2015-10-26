// Patterns: 1
// Matches: Foo.cs
// NotMatches: Bar.cs 

using StructureMap;
using TestApplication.Types;

namespace TestApplication.StructureMap.ContainerTests
{
    public class ForNonGenericAddNonGenericTypeOf
    {
        public ForNonGenericAddNonGenericTypeOf()
        {
            var container = new Container(x => x.For(typeof(IFoo)).Add(typeof(Foo)));
        } 
    }
}