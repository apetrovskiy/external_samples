﻿using System.IO;
using NBehave.Narrator.Framework.Extensions;
using NUnit.Framework;

namespace NBehave.Narrator.Framework.Specifications.System.Specs
{
    [TestFixture]
    public class WhenRunningAScenarioWithArguments : SystemTestContext
    {
        private FeatureResults _results;

        protected override void EstablishContext()
        {
            Configure_With(@"System.Specs\Scenarios\ScenarioWithArguments.feature");
        }

        protected override void Because()
        {
            _results = _config.Build().Run();
        }

        [Test]
        public void AllStepsShouldPass()
        {
            Assert.That(_results.NumberOfPassingScenarios, Is.EqualTo(1));
        }
    }

    [ActionSteps]
    public class ScenarioStepsWithArguments
    {
        [Given("a scenario that has $arguments")]
        public void Given(string arguments)
        {
            Assert.That(arguments, Is.EqualTo("arguments"));
        }

        [When("the scenario with arguments is $executed")]
        public void When(string executed)
        {
            Assert.That(executed, Is.EqualTo("executed"));
        }

        [Then(@"the scenario with arguments should $pass")]
        public void Then(string pass)
        {
            Assert.That(pass, Is.EqualTo("pass"));
        }
    }
}