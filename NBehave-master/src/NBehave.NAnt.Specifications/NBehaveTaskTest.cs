using System;
using System.IO;
using NAnt.Core;
using NUnit.Framework;

namespace NBehave.NAnt.Specifications
{
    [TestFixture]
    public class NBehaveTaskTest
    {
        private TextWriter outStream;

        [SetUp]
        public void CaptureConsoleOut()
        {
            outStream  = Console.Out;
            Console.SetOut(new StringWriter());
        }

        [TearDown]
        public void ResetConsoleOut()
        {
            Console.SetOut(outStream);
        }

        [Test]
        public void Execute_tests_in_test_build_script()
        {
            var project = new Project("NBehaveTestScript.build", Level.Info, 1);
            Assert.IsTrue(project.Run(), "Something went wrong executing the test script.  Check log.");
        }
    }
}
