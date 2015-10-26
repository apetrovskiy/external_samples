// Patterns: 1
// Matches: CommonImpl1.cs
// NotMatches: Foo.cs

using Autofac;
using TestApplication.Types;

namespace TestApplication.Autofac
{
    public class RegisterTypeNonGeneric : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(CommonImpl1));
        }
    }
}