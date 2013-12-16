﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using NBehave.Narrator.Framework.Extensions;
using NUnit.Framework;

namespace NBehave.Narrator.Framework.Specifications.System.Specs
{
    [TestFixture]
    public class WhenRunningAScenarioWithABackgroundSection : SystemTestContext
    {
        private FeatureResults _results;

        protected override void EstablishContext()
        {
            Configure_With(@"System.Specs\Backgrounds\Background.feature");
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
    public class ScenarioStepsForBackground
    {
        private int callCount;

        [Given("this background section declaration")]
        public void FirstGiven()
        {
            Assert.That(callCount, Is.EqualTo(0));
            callCount++;
        }

        [Given("this one")]
        public void SecondGiven()
        {
            Assert.That(callCount, Is.EqualTo(1));
            callCount++;
        }

        [Given("this scenario under the context of a background section")]
        public void ThirdGiven()
        {
            Assert.That(callCount, Is.EqualTo(2));
            callCount++;
        }

        [When("the scenario with a background section is executed")]
        public void When()
        {
            Assert.That(callCount, Is.EqualTo(3));
            callCount++;
        }

        [Then("the background section steps should be called before this scenario")]
        public void AnotherThen()
        {
            Assert.That(callCount, Is.EqualTo(4));
            callCount++;
        }
    }
}