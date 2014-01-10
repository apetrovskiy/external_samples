﻿using System;
using Xunit;
using Xunit.Extensions;
using TestResult = Xunit.Sdk.TestResult;

namespace XunitContrib.Runner.ReSharper.RemoteRunner.Tests.When_running_tests
{
    public class When_finishing_a_test : SingleClassTestRunContext
    {
        [Fact]
        public void Should_report_duration_for_passing_test()
        {
            var duration = TimeSpan.FromMinutes(12.34);
            var method = testClass.AddPassingTest("Method1");

            SetResultInspectorToUpdateDuration(duration);

            Run();

            Messages.OfTask(method.Task).AssertTaskDuration(duration);
        }

        [Fact]
        public void Should_report_duration_for_failing_test()
        {
            var duration = TimeSpan.FromMinutes(12.34);
            var method = testClass.AddFailingTest("TestMethod1", new NotImplementedException());

            SetResultInspectorToUpdateDuration(duration);

            Run();

            Messages.OfTask(method.Task).AssertTaskDuration(duration);
        }

        [Fact]
        public void Should_report_duration_for_passing_theory()
        {
            var duration = TimeSpan.FromMinutes(12.34);
            var method = testClass.AddMethod("TestMethod1", _ => { }, new[] { Parameter.Create<int>("value") },
                                             new TheoryAttribute(),
                                             new InlineDataAttribute(12), new InlineDataAttribute(33));
            var theoryTask = method.TheoryTasks[0];

            SetResultInspectorToUpdateDuration(duration);

            Run();

            Messages.OfEquivalentTask(theoryTask).AssertTaskDuration(duration);
        }

        [Fact]
        public void Should_report_duration_for_failing_theory()
        {
            var duration = TimeSpan.FromMinutes(12.34);
            var method = testClass.AddMethod("TestMethod1", _ =>
                                             {
                                                throw new Exception();
                                             },
                                             new[] {Parameter.Create<int>("value")},
                                             new TheoryAttribute(),
                                             new InlineDataAttribute(12), new InlineDataAttribute(33));
            var theoryTask = method.TheoryTasks[0];

            SetResultInspectorToUpdateDuration(duration);

            Run();

            Messages.OfEquivalentTask(theoryTask).AssertTaskDuration(duration);
        }

        [Fact]
        public void Should_not_report_duration_for_skipped_test()
        {
            var method = testClass.AddSkippedTest("TestMethod1", "Just because");

            Run();

            Messages.OfTask(method.Task).AssertNoAction(ServerAction.TaskDuration);
        }

        private void SetResultInspectorToUpdateDuration(TimeSpan duration)
        {
            ResultInspector = result =>
                {
                    var testResult = result as TestResult;
                    if (testResult != null) testResult.ExecutionTime = duration.TotalSeconds;
                    return result;
                };
        }
    }
}