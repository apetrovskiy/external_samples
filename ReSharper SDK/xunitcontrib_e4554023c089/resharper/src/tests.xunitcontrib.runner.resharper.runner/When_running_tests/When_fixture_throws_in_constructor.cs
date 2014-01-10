using System;
using System.Linq;
using JetBrains.ReSharper.TaskRunnerFramework;
using Xunit;

namespace XunitContrib.Runner.ReSharper.RemoteRunner.Tests.When_running_tests
{
    public class When_fixture_throws_in_constructor
    {
        private readonly TestRun testRun;
        private readonly Class testClass;

        private class ThrowsInConstructor
        {
            public static Exception Exception;

            public ThrowsInConstructor()
            {
                throw Exception;
            }
        }

        public When_fixture_throws_in_constructor()
        {
            testRun = new TestRun();
            testClass = testRun.AddClass("TestNamespace.TestClass1");
            testClass.AddFixture<ThrowsInConstructor>();
            ThrowsInConstructor.Exception = new InvalidOperationException("Ooops");
            testClass.AddPassingTest("TestMethod1");
            testClass.AddPassingTest("TestMethod2");
        }

        [Fact]
        public void Should_notify_class_start()
        {
            testRun.Run();

            testRun.Messages.OfTask(testClass.ClassTask).AssertTaskStarting();
        }

        [Fact]
        public void Should_notify_class_exception()
        {
            testRun.Run();

            testRun.Messages.OfTask(testClass.ClassTask).AssertTaskException(ThrowsInConstructor.Exception);
        }

        [Fact]
        public void Should_notify_class_failed()
        {
            testRun.Run();

            testRun.Messages.OfTask(testClass.ClassTask).AssertTaskFinishedBadly(ThrowsInConstructor.Exception);
        }

        [Fact]
        public void Should_notify_all_methods_as_finished_and_failed()
        {
            testRun.Run();

            var methodTasks = testClass.Methods.Select(m => m.Task).ToList();

            var taskException = new TaskException(null, string.Format("Class failed in {0}", testClass.ClassTask.TypeName), null);
            testRun.Messages.OfTask(methodTasks[0]).AssertTaskException(taskException);
            testRun.Messages.OfTask(methodTasks[1]).AssertTaskException(taskException);
        }

        [Fact]
        public void Should_continue_running_other_classes()
        {
            var testClass2 = testRun.AddClass("TestNamespace.TestClass2");
            var testMethod = testClass2.AddPassingTest("OtherMethod");

            testRun.Run();

            testRun.Messages.OfTask(testClass2.ClassTask).AssertTaskStarting();
            testRun.Messages.OfTask(testMethod.Task).AssertOrderedActions(ServerAction.TaskStarting, ServerAction.TaskFinished);
            testRun.Messages.OfTask(testClass2.ClassTask).AssertTaskFinishedSuccessfully();
        }
    }
}