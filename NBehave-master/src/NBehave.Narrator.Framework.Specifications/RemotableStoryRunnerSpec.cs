using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using NBehave.Narrator.Framework.EventListeners;
using NBehave.Narrator.Framework.EventListeners.Xml;
using NBehave.Narrator.Framework.Internal;
using NBehave.Narrator.Framework.Remoting;
using NUnit.Framework;
using TestPlainTextAssembly;
using Context = NUnit.Framework.TestFixtureAttribute;
using Specification = NUnit.Framework.TestAttribute;

namespace NBehave.Narrator.Framework.Specifications
{
    [Context]
    public abstract class RemotableStoryRunnerSpec
    {
        [Explicit("This test crashes the R# test runner")]
        public void RawDeserialization()
        {
            object o;
            using (var fileStream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.None))
            {
                o = new BinaryFormatter()
                    .Deserialize(fileStream);
            }

            var result = o as ScenarioResult;
            Assert.IsNotNull(result);
        }

        private string tempFileName;

        private IRunner CreateTextRunner(IEnumerable<string> assemblies)
        {
            return CreateTextRunner(assemblies, null);
        }

        private IRunner CreateTextRunner(IEnumerable<string> assemblies, string scenarioText)
        {
            var writer = new StreamWriter(new MemoryStream());
            var listener = new TextWriterEventListener(writer);
            return CreateTextRunner(assemblies, listener, scenarioText);
        }

        private IRunner CreateTextRunner(IEnumerable<string> assemblies, IEventListener listener, string scenarioText)
        {
            var configuration = NBehaveConfiguration.New
                                                    .SetAssemblies(assemblies)
                                                    .SetEventListener(listener);

            if (!String.IsNullOrEmpty(scenarioText))
            {
                // Configure the scenario to get run
                tempFileName = Path.GetTempFileName();
                using (var fileStream = new StreamWriter(File.Create(tempFileName)))
                {
                    fileStream.Write(scenarioText);
                }
                configuration.SetScenarioFiles(new[] { tempFileName });
            }
            return RunnerFactory.CreateTextRunner(configuration);
        }

        private void SetupConfigFile()
        {
            File.WriteAllText(Path.Combine(GetAssemblyLocation(), "TestPlainTextAssembly.dll.config"),
                            "<configuration><appSettings><add key=\"foo\" value=\"bar\" /></appSettings></configuration>");
        }

        private void DeleteConfigFile()
        {
            File.Delete(Path.Combine(GetAssemblyLocation(), "TestPlainTextAssembly.dll.config"));
            if (!String.IsNullOrEmpty(tempFileName))
                File.Delete(tempFileName);
        }

        private string GetAssemblyLocation()
        {
            var assemblyPath = typeof(ConfigFileActionSteps).Assembly
                                                             .CodeBase
                                                             .Replace("file:///", "");
            return Path.GetDirectoryName(assemblyPath);
        }

        [Context]
        public class When_creating_a_runner_with_config_file : RemotableStoryRunnerSpec
        {
            private IRunner runner;

            [SetUp]
            public void SetUp()
            {
                SetupConfigFile();
                runner = CreateTextRunner(new[] { "TestPlainTextAssembly.dll" });
            }

            [TearDown]
            public void TearDown()
            {
                DeleteConfigFile();
            }

            [Specification]
            public void Should_construct_runner_suited_for_remoting()
            {
                Assert.IsInstanceOf(typeof(AppDomainRunner), runner);
            }
        }

        [Context]
        public class When_running_plain_text_scenarios_with_config_file : RemotableStoryRunnerSpec
        {
            private IRunner runner;
            private FeatureResults results;

            [SetUp]
            public void SetUp()
            {
                var scenarioText = "Feature: Config file support\r\n" +
                                    "Scenario: Reading values from a config file\r\n" +
                                    "Given an assembly with a matching configuration file\r\n" +
                                    "When the value of setting foo is read\r\n" +
                                    "Then the value should be bar";
                SetupConfigFile();
                runner = CreateTextRunner(new[] { "TestPlainTextAssembly.dll" }, scenarioText);

                results = runner.Run();

            }

            [TearDown]
            public void TearDown()
            {
                DeleteConfigFile();
            }

            [Specification]
            public void Should_read_values_from_the_appropriate_config_file()
            {
                Assert.AreEqual(1, results.NumberOfPassingScenarios);
                Assert.AreEqual(0, results.NumberOfFailingScenarios);
            }
        }

        [Context]
        public class When_running_failing_plain_text_scenarios_with_config_file : RemotableStoryRunnerSpec
        {
            private IRunner runner;
            private FeatureResults results;

            [SetUp]
            public void SetUp()
            {
                var scenarioText = "Feature: Config file support\r\n" +
                                    "Scenario: Reading values from a config file\r\n" +
                                    "Given an assembly with a matching configuration file\r\n" +
                                    "When the value of setting foo is read\r\n" +
                                    "Then the value should be meeble";
                SetupConfigFile();
                runner = CreateTextRunner(new[] { "TestPlainTextAssembly.dll" }, scenarioText);

                results = runner.Run();

            }

            [TearDown]
            public void TearDown()
            {
                DeleteConfigFile();
            }

            [Explicit("This test crashes the R# test runner")]
            public void Should_read_values_from_the_appropriate_config_file()
            {
                //WARNING: This test crashes the R# test runner, v 5.1.3000.12 anyway
                Assert.AreEqual(0, results.NumberOfPassingScenarios);
                Assert.AreEqual(1, results.NumberOfFailingScenarios);
            }
        }

        [Context]
        public class When_running_text_scenarios_with_no_feature_and_config_file : RemotableStoryRunnerSpec
        {
            private IRunner _runner;

            [SetUp]
            public void SetUp()
            {
                var scenarioText = "Scenario: Reading values from a config file\r\n" +
                                    "Given an assembly with a matching configuration file\r\n" +
                                    "When the value of setting foo is read\r\n" +
                                    "Then the value should be bar";
                SetupConfigFile();
                _runner = CreateTextRunner(new[] { "TestPlainTextAssembly.dll" }, scenarioText);
            }

            [TearDown]
            public void TearDown()
            {
                DeleteConfigFile();
            }

            [Specification]
            public void Should_run_scenario()
            {
                var result = _runner.Run();
                Assert.That(result.NumberOfFailingScenarios, Is.EqualTo(0));
                Assert.That(result.NumberOfPendingScenarios, Is.EqualTo(0));
                Assert.That(result.NumberOfPassingScenarios, Is.EqualTo(1));
            }
        }

        [Context]
        public class When_running_plain_text_scenarios_with_listener_and_config_file : RemotableStoryRunnerSpec
        {
            private IRunner runner;
            private FeatureResults results;
            private XmlDocument xmlOut;
            private const string FeatureTitle = "Scenario runner that can read from its own config file";

            [TestFixtureSetUp]
            public void SetUp()
            {
                var writer = new XmlTextWriter(new MemoryStream(), Encoding.UTF8);
                var listener = new XmlOutputEventListener(writer);

                const string scenarioText = "Feature: " + FeatureTitle + "\r\n" +
                                            "Scenario: Reading values from a config file\r\n" +
                                            "Given an assembly with a matching configuration file\r\n" +
                                            "When the value of setting foo is read\r\n" +
                                            "Then the value should be bar";

                SetupConfigFile();
                runner = CreateTextRunner(new[] { "TestPlainTextAssembly.dll" }, listener, scenarioText);

                results = runner.Run();

                xmlOut = new XmlDocument();
                writer.BaseStream.Seek(0, SeekOrigin.Begin);
                xmlOut.Load(writer.BaseStream);
            }

            [TestFixtureTearDown]
            public void TearDown()
            {
                DeleteConfigFile();
            }

            [Specification]
            public void Should_read_values_from_the_appropriate_config_file()
            {
                Assert.AreEqual(1, results.NumberOfPassingScenarios);
            }

            [Specification]
            public void Should_find_one_feature()
            {
                var storyNodes = xmlOut.SelectNodes("//feature");
                Assert.That(storyNodes.Count, Is.EqualTo(1));
            }

            [Specification]
            public void Should_set_title_of_feature()
            {
                var storyNodes = xmlOut.SelectSingleNode("//feature").Attributes["name"];

                Assert.That(storyNodes.Value, Is.EqualTo(FeatureTitle));
            }

            [Specification]
            public void Should_run_one_scenario()
            {
                var scenarioNodes = xmlOut.SelectNodes("//scenario");

                Assert.That(scenarioNodes.Count, Is.EqualTo(1));
            }
        }
    }
}