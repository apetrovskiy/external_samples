﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using NBehave.Narrator.Framework.Extensions;
using NBehave.Narrator.Framework.Internal;
using NUnit.Framework;

namespace NBehave.Narrator.Framework.Specifications
{
    [TestFixture]
    public abstract class StringStepRunnerSpec
    {
        private IStringStepRunner runner;
        private ActionCatalog actionCatalog;

        public virtual void Setup()
        {
            actionCatalog = new ActionCatalog();
            runner = new StringStepRunner(actionCatalog);
        }

        [TestFixture]
        public class WhenRunningPlainTextScenarios : StringStepRunnerSpec
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();
            }

            [Test]
            public void ShouldInvokeActionGivenATokenString()
            {
                var wasCalled = false;
                Action<string> action = name => { wasCalled = true; };
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"my name is (?<name>\w+)"), action, action.Method, "Given"));
                runner.Run(new StringStep("Given my name is Morgan", ""));
                Assert.IsTrue(wasCalled, "Action was not called");
            }

            [Test]
            public void ShouldGetParameterValueForAction()
            {
                var actual = string.Empty;
                Action<string> action = name => { actual = name; };
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"my name is (?<name>\w+)"), action, action.Method, "Given"));
                runner.Run(new StringStep("Given my name is Morgan", ""));
                Assert.That(actual, Is.EqualTo("Morgan"));
            }

            [Test]
            public void ShouldReturnPendingIfActionGivenInTokenStringDoesntExist()
            {
                var step = new StringStep("Given this doesnt exist", "");
                runner.Run(step);
                Assert.That(step.StepResult.Result, Is.TypeOf(typeof(PendingNotImplemented)));
            }

            public class UserClass
            {
                public int Age { get; set; }
                public string Name { get; set; }
            }

            [Test]
            public void Should_get_parameter_value_for_action_with_parameter_of_complex_type()
            {
                UserClass actual = null;
                Action<UserClass> action = _ => { actual = _; };
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"my name is (?<name>\w+) and I'm (?<age>\d+) years old"), action, action.Method, "Given"));
                runner.Run(new StringStep("Given my name is Morgan and I'm 42 years old", ""));
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Name, Is.EqualTo("Morgan"));
                Assert.That(actual.Age, Is.EqualTo(42));
            }

            [Test]
            public void Should_get_parameter_value_for_action_with_parameter_of_List_of_complex_type()
            {
                List<UserClass> actual = null;
                Action<List<UserClass>> action = _ => { actual = _; };
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"some users:"), action, action.Method, "Given"));
                var tableStep = new StringTableStep("Given some users:", "");
                tableStep.AddTableStep(new Example(new ExampleColumns { new ExampleColumn("age"), new ExampleColumn("name") }, new Dictionary<string, string> { { "age", "42" }, { "name", "Morgan" } }));
                tableStep.AddTableStep(new Example(new ExampleColumns { new ExampleColumn("age"), new ExampleColumn("name") }, new Dictionary<string, string> { { "age", "666" }, { "name", "Lucifer" } }));
                runner.Run(tableStep);
                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Count, Is.EqualTo(2));
                Assert.That(actual[0].Name, Is.EqualTo("Morgan"));
                Assert.That(actual[0].Age, Is.EqualTo(42));
                Assert.That(actual[1].Name, Is.EqualTo("Lucifer"));
                Assert.That(actual[1].Age, Is.EqualTo(666));
            }
        }

        [TestFixture, ActionSteps]
        public class WhenClassWithActionStepsImplementsNotificationAttributes : StringStepRunnerSpec
        {
            private bool _beforeScenarioWasCalled;
            private bool _beforeStepWasCalled;
            private bool _afterStepWasCalled;
            private bool _afterScenarioWasCalled;

            [Given(@"something$")]
            public void GivenSomething()
            { }

            [BeforeScenario]
            public void OnBeforeScenario()
            {
                _beforeScenarioWasCalled = true;
            }

            [BeforeStep]
            public void OnBeforeStep()
            {
                _beforeStepWasCalled = true;
            }

            [AfterStep]
            public void OnAfterStep()
            {
                _afterStepWasCalled = true;
            }

            [AfterScenario]
            public void OnAfterScenario()
            {
                _afterScenarioWasCalled = true;
            }

            [SetUp]
            public override void Setup()
            {
                base.Setup();

                Action action = GivenSomething;
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"something"), action, action.Method, "Given", this));

                _beforeScenarioWasCalled = false;
                _beforeStepWasCalled = false;
                _afterStepWasCalled = false;
                _afterScenarioWasCalled = false;
            }

            [Test]
            public void RunningAStepShouldCallMostAttributedMethods()
            {
                var actionStepText = new StringStep("something", "");
                runner.Run(actionStepText);

                Assert.That(_beforeScenarioWasCalled);
                Assert.That(_beforeStepWasCalled);
                Assert.That(_afterStepWasCalled);
                Assert.That(!_afterScenarioWasCalled);
            }

            [Test]
            public void CompletingAScenarioShouldCallAllAttributedMethods()
            {
                var actionStepText = new StringStep("something", "");
                runner.Run(actionStepText);
                runner.OnCloseScenario();

                Assert.That(_beforeScenarioWasCalled);
                Assert.That(_beforeStepWasCalled);
                Assert.That(_afterStepWasCalled);
                Assert.That(_afterScenarioWasCalled);
            }
        }

        [TestFixture, ActionSteps]
        public class When_AfterStep_throws_exception : StringStepRunnerSpec
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();

                Action action = GivenSomething;
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"something"), action, action.Method, "Given", this));
            }

            [Test]
            public void RunningAStepShouldCallMostAttributedMethods()
            {
                var step = new StringStep("something", "");
                runner.Run(step);
                Assert.That(step.StepResult.Result, Is.InstanceOf<Failed>());
                Assert.That(step.StepResult.Message, Is.StringContaining("ArgumentNullException"));
            }

            [Given(@"When_AfterStep_throws_exception$")]
            public void GivenSomething()
            { }

            [AfterStep]
            public void AfterStep()
            {
                throw new ArgumentNullException("AfterStep");
            }

        }

        [TestFixture, ActionSteps]
        public class When_BeforeStep_throws_exception : StringStepRunnerSpec
        {
            private bool _afterStepWasCalled;

            [SetUp]
            public override void Setup()
            {
                base.Setup();

                Action action = GivenSomething;
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"something"), action, action.Method, "Given", this));
            }

            [Test]
            public void RunningAStepShouldCallMostAttributedMethods()
            {
                var step = new StringStep("something", "");
                runner.Run(step);
                Assert.That(step.StepResult.Result, Is.InstanceOf<Failed>());
                Assert.That(step.StepResult.Message, Is.StringContaining("ArgumentNullException"));
            }

            [Given(@"When_AfterStep_throws_exception$")]
            public void GivenSomething()
            { }

            [BeforeStep]
            public void BeforeStep()
            {
                throw new ArgumentNullException("BeforeStep");
            }

            [AfterStep]
            public void AfterStep()
            {
                _afterStepWasCalled = true;
            }

        }

        [TestFixture, ActionSteps]
        public class When_first_step_throws_exception : StringStepRunnerSpec
        {
            private bool _afterStepWasCalled;

            [SetUp]
            public override void Setup()
            {
                base.Setup();

                Action action = GivenSomething;
                actionCatalog.Add(new ActionMethodInfo(new Regex(@"something"), action, action.Method, "Given", this));
            }

            [Test]
            public void Should_Call_afterStep_if_step_fails()
            {
                var step = new StringStep("something", "");
                runner.Run(step);
                Assert.That(_afterStepWasCalled, Is.True);
            }

            [Given(@"something")]
            public void GivenSomething()
            {
                throw new NotImplementedException("fail");
            }

            [AfterStep]
            public void AfterStep()
            {
                _afterStepWasCalled = true;
            }
        }

        [TestFixture]
        public class When_running_with_step_that_has_docstring : StringStepRunnerSpec
        {
            [SetUp]
            public override void Setup()
            {
                base.Setup();
            }

            [Test]
            public void ShouldInvokeActionGivenATokenString()
            {
                var wasCalled = false;
                var docString = "";
                Action<int, string> action = (value, thisIsThedocString) =>
                {
                    wasCalled = true;
                    docString = thisIsThedocString;
                };
                actionCatalog.Add(new ActionMethodInfo("a value $value followed by docstring".AsRegex(), action, action.Method, "Given"));
                var stringStep = new StringStep("Given a value 42 followed by docstring", "");
                stringStep.AddDocString("docString");
                runner.Run(stringStep);
                Assert.IsTrue(wasCalled, "Action was not called");
                Assert.AreEqual("docString", docString);
            }
        }
    }
}
