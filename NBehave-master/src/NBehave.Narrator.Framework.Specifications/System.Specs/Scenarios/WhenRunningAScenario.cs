﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using NBehave.Narrator.Framework.Extensions;
using NUnit.Framework;

namespace NBehave.Narrator.Framework.Specifications.System.Specs
{
    [TestFixture]
    public class WhenRunningAScenario : SystemTestContext
    {
        private FeatureResults _results;

        protected override void EstablishContext()
        {
           Configure_With(@"System.Specs\Scenarios\Scenario.feature");
        }

        protected override void Because()
        {
            _results = _config.Build().Run();
        }

        [Test]
        public void AllStepsShouldPass()
        {
            IEnumerable<StepResult> enumerable = _results.SelectMany(_=>_.ScenarioResults).SelectMany(result => result.StepResults);
            IEnumerable<Result> results = enumerable.Select(stepResult => stepResult.Result);

            foreach (var result in results)
            {
                Assert.That(result, Is.TypeOf(typeof (Passed)), result.Message);
            }
        }
    }

    [ActionSteps]
    public class ScenarioSteps
    {
        [Given("this plain scenario")]
        public void Given()
        {
        }

        [Given("this second scenario")]
        public void AnotherGiven()
        {
        }

        [When("this plain scenario is executed")]
        public void When()
        {
        }

        [When("the second scenario is executed")]
        public void SecondWhen()
        {
        }

        [Then("this plain scenario should pass")]
        public void Then()
        {
        }

        [Then("it should also pass")]
        public void AnotherThen()
        {
        }
    }
}