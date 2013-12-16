using System.IO;
using System.Linq;
using NBehave.Narrator.Framework.Remoting;

namespace NBehave.Narrator.Framework.Internal
{
    public static class RunnerFactory
    {
        public static IRunner CreateTextRunner(NBehaveConfiguration configuration)
        {
            var assemblyWithConfigFile = configuration.Assemblies
                                                      .Where(path => File.Exists(path + ".config"))
                                                      .Select(path => path + ".config")
                                                      .FirstOrDefault();
            if (assemblyWithConfigFile == null)
                assemblyWithConfigFile = configuration.Assemblies.First() + ".config";

            if (configuration.CreateAppDomain == false)
                return new TextRunner(configuration);

            return new AppDomainRunner(configuration, assemblyWithConfigFile);    
        }
    }
}