using System.Collections.Generic;
using NBehave.Narrator.Framework.Internal;

namespace NBehave.Narrator.Framework
{
    public class ScenarioContext : NBehaveContext
    {
        public FeatureContext FeatureContext { get; private set; }
        public string ScenarioTitle { get { return Scenario.Title; } }
        internal Scenario Scenario { get; set; }

        public ScenarioContext(FeatureContext featureContext, Scenario scenario)
        {
            FeatureContext = featureContext;
            Scenario = scenario;
        }

        public ScenarioContext(FeatureContext featureContext, Scenario scenario, IEnumerable<string> tags)
            : this(featureContext, scenario)
        {
            AddTags(tags);
        }

        public static ScenarioContext Current
        {
            get { return Tiny.TinyIoCContainer.Current.Resolve<ScenarioContext>(); }
        }

        public override string ToString()
        {
            return ScenarioTitle;
        }
    }
}