﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NBehave.Narrator.Framework.TextParsing;
using NUnit.Framework;

namespace NBehave.Narrator.Framework.Specifications
{
    [TestFixture]
    public abstract class ScenarioParserSpec
    {
        private List<Scenario> scenarios;
        private string featureFileName;

        private GherkinScenarioParser CreateScenarioParser()
        {
            scenarios = new List<Scenario>();
            return new GherkinScenarioParser(NBehaveConfiguration.New);
        }

        private StringStep NewStringStep(string step)
        {
            return new StringStep(step, featureFileName);
        }

        private void Parse(string scenario)
        {
            if (!scenario.StartsWith("Feature"))
            {
                scenario = scenario.Insert(0, "Feature: Parsing feature files" + Environment.NewLine +
                                              "    As a parser" + Environment.NewLine +
                                              "    I want to be able to parse files" + Environment.NewLine +
                                              "    So that I can build a domain model" + Environment.NewLine);
            }

            featureFileName = Path.GetTempFileName();

            using (var fileStream = new StreamWriter(File.Create(featureFileName)))
            {
                fileStream.Write(scenario);
            }

            var parser = CreateScenarioParser();
            parser.ScenarioEvent += (s, e) => scenarios.Add(e.EventInfo);
            parser.Parse(featureFileName);
        }

        [TestFixture]
        public class ScenarioSimpleScenarioWithoutTitle : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario =
                    "Scenario: Adding numbers" + Environment.NewLine +
                    "  Given numbers 1 and 2" + Environment.NewLine +
                    "  When I add the numbers" + Environment.NewLine +
                    "  Then the sum is 3";

                Parse(scenario);
            }

            [Test]
            public void ShouldHaveGivenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("Given numbers 1 and 2"));
            }

            [Test]
            public void ShouldHaveWhenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("When I add the numbers"));
            }

            [Test]
            public void ShouldHaveThenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("Then the sum is 3"));
            }
        }

        public class ScenarioSimpleScenarioWithTitle : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario = "Scenario: Adding numbers" + Environment.NewLine +
                               "  Given numbers 1 and 2" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 3";

                Parse(scenario);
            }

            [Test]
            public void ShouldFind3Steps()
            {
                Assert.That(scenarios.First().Steps.Count(), Is.EqualTo(3));
            }

            [Test]
            public void ShouldHaveAScenarioTitle()
            {
                Assert.That(scenarios.First().Title, Is.EqualTo("Adding numbers"));
            }
        }

        public class ScenarioTwoScenariosWithTitle : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario = "Scenario: Adding numbers" + Environment.NewLine +
                               "  Given numbers 1 and 2" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 3" + Environment.NewLine +
                               Environment.NewLine +
                               "Scenario: Adding numbers again" + Environment.NewLine +
                               "  Given numbers 3 and 5" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 8";

                Parse(scenario);
            }

            [Test]
            public void ShouldFind2Scenarios()
            {
                Assert.That(scenarios.Count(), Is.EqualTo(2));
            }

            [Test]
            public void ShouldHaveAScenarioTitleOnBothScenarios()
            {
                Assert.That(scenarios.First().Title, Is.EqualTo("Adding numbers"));
                Assert.That(scenarios.Skip(1).First().Title, Is.EqualTo("Adding numbers again"));
            }
        }

        public class ScenarioFeatureWithScenario : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario = "Feature: Calculator" + Environment.NewLine +
                               "Scenario: Adding numbers" + Environment.NewLine +
                               "  Given numbers 1 and 2" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 3" + Environment.NewLine +
                               Environment.NewLine +
                               "Scenario: Adding numbers again" + Environment.NewLine +
                               "  Given numbers 3 and 5" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 8";

                Parse(scenario);
            }

            [Test]
            public void ShouldFind2Scenarios()
            {
                Assert.That(scenarios.Count(), Is.EqualTo(2));
            }

            [Test]
            public void ShouldHaveAFeatureTitle()
            {
                Assert.That(scenarios.First().Feature.Title, Is.EqualTo("Calculator"));
            }
        }

        public class ScenarioFeatureNarrativeIsAllTextUptoNextStepKeyWord : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario = "Feature: Calculator" + Environment.NewLine +
                               "  This is the narrative" + Environment.NewLine +
                               "  This is second row of narrative" + Environment.NewLine +
                               "Scenario: Adding numbers" + Environment.NewLine +
                               "  Given numbers 1 and 2" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 3" + Environment.NewLine +
                               Environment.NewLine +
                               "Scenario: Adding numbers again" + Environment.NewLine +
                               "  Given numbers 3 and 5" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 8";

                Parse(scenario);
            }

            [Test]
            public void ShouldHaveNarrative()
            {
                var narrative = scenarios.First().Feature.Narrative;
                StringAssert.Contains("This is the narrative" + Environment.NewLine, narrative);
                StringAssert.Contains("This is second row of narrative", narrative);
            }

            [Test]
            public void ShouldFind2Scenarios()
            {
                Assert.That(scenarios.Count(), Is.EqualTo(2));
            }

            [Test]
            public void ShouldHaveAFeatureTitle()
            {
                Assert.That(scenarios.First().Feature.Title, Is.EqualTo("Calculator"));
            }
        }

        public class ScenarioScenarioWithExampleTable : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario = "Scenario: Adding numbers" + Environment.NewLine +
                               "  Given numbers [left] and [right]" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is [sum]" + Environment.NewLine +
                               Environment.NewLine +
                               "Examples:" + Environment.NewLine +
                               "|left|right|sum|" + Environment.NewLine +
                               "|1|2|3|" + Environment.NewLine +
                               "|2|3|5|";

                Parse(scenario);
            }

            [Test]
            public void ScenarioShouldHaveTwoExamples()
            {
                Assert.That(scenarios.First().Examples.Count(), Is.EqualTo(2));
            }

            [Test]
            public void ShouldFind3Steps()
            {
                Assert.That(scenarios.First().Steps.Count(), Is.EqualTo(3));
            }

            [Test]
            public void ShouldHaveAScenarioTitle()
            {
                Assert.That(scenarios.First().Title, Is.EqualTo("Adding numbers"));
            }

            [Test]
            public void ShouldHaveGivenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("Given numbers [left] and [right]"));
            }

            [Test]
            public void ShouldHaveThenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("Then the sum is [sum]"));
            }
        }

        public class ScenarioScenarioWithTableOnGiven : ScenarioParserSpec
        {
            private StringTableStep givenStep;
            private StringTableStep thenStep;

            [SetUp]
            public void Scenario()
            {
                var scenario =
                    "Scenario: A scenario with an inline table" + Environment.NewLine +
                    "  Given the following people exists:" + Environment.NewLine +
                    "  |Name          |Country|" + Environment.NewLine +
                    "  |Morgan Persson|Sweden |" + Environment.NewLine +
                    "  |Jimmy Nilsson |Sweden |" + Environment.NewLine +
                    "  |Jimmy bogard  |USA    |" + Environment.NewLine +
                    "  When I search for people in sweden" + Environment.NewLine +
                    "  Then I should get:" + Environment.NewLine +
                    "  |Name          |" + Environment.NewLine +
                    "  |Morgan Persson|" + Environment.NewLine +
                    "  |Jimmy Nilsson |";

                Parse(scenario);
                givenStep = scenarios.First().Steps.First() as StringTableStep;
                thenStep = scenarios.First().Steps.Last() as StringTableStep;
            }

            [Test]
            public void GivenStepShouldHaveThreeTableSteps()
            {
                Assert.That(givenStep.TableSteps.Count(), Is.EqualTo(3));
            }

            [Test]
            public void ThenStepShouldHaveTwoTableSteps()
            {
                Assert.That(thenStep.TableSteps.Count(), Is.EqualTo(2));
            }

            [Test]
            public void TableStepColumnNamesShouldBeStoredInLowerCase()
            {
                var step = givenStep.TableSteps.First();
                CollectionAssert.Contains(step.ColumnNames, new ExampleColumn("Name"));
                Assert.That(step.ColumnValues["Name"], Is.Not.Null);
            }

            [Test]
            public void ShouldHaveGivenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("Given the following people exists:"));
            }

            [Test]
            public void ShouldHaveWhenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("When I search for people in sweden"));
            }

            [Test]
            public void ShouldHaveThenStep()
            {
                CollectionAssert.Contains(scenarios.First().Steps, NewStringStep("Then I should get:"));
            }
        }

        public class ScenarioMultipleFeatures : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario = "Feature: Calculator 1" + Environment.NewLine +
                               "Scenario: Adding numbers 1" + Environment.NewLine +
                               "  Given numbers 1 and 2" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 3" + Environment.NewLine +
                               "" + Environment.NewLine +
                               "Feature: Calculator 2" + Environment.NewLine +
                               "Scenario: Adding numbers 2" + Environment.NewLine +
                               "  Given numbers 1 and 2" + Environment.NewLine +
                               "  When I add the numbers" + Environment.NewLine +
                               "  Then the sum is 3";

                Parse(scenario);
            }

            [Test]
            public void Feature1ShouldBeReferencedByScenario2()
            {
                Assert.That(scenarios.First().Feature.Title, Is.EqualTo("Calculator 1"));
            }

            [Test]
            public void Feature2ShouldBeReferencedByScenario2()
            {
                Assert.That(scenarios.Skip(1).First().Feature.Title, Is.EqualTo("Calculator 2"));
            }
        }

        public class ScenarioWithBackground : ScenarioParserSpec
        {
            [SetUp]
            public void Scenario()
            {
                var scenario = "Feature: Support for background sections                                   " + Environment.NewLine +
                               "  As a NBehave user                                                        " + Environment.NewLine +
                               "  I want to be able to declare background sections                         " + Environment.NewLine +
                               "  So that I can add context to my scenarios                                " + Environment.NewLine +
                               "                                                                           " + Environment.NewLine +
                               "  Background:                                                              " + Environment.NewLine +
                               "    Given this background section declaration                              " + Environment.NewLine +
                               "    And this one                                                           " + Environment.NewLine +
                               "                                                                           " + Environment.NewLine +
                               "  Scenario: Running a feature file with a background section               " + Environment.NewLine +
                               "    Given this scenario under the context of a background section          " + Environment.NewLine +
                               "    When the scenario is executed                                          " + Environment.NewLine +
                               "    Then the background section steps should be called before this scenario" + Environment.NewLine;

                Parse(scenario);
            }

            [Test]
            public void ShouldHaveAddedBackgroundGivenStepToFeature()
            {
                var background = scenarios.First().Feature.Background;
                Assert.That(background.Steps.First().Step, Is.EqualTo("Given this background section declaration"));
            }

            [Test]
            public void ShouldHaveAddedBackgroundAndStepToFeature()
            {
                var background = scenarios.First().Feature.Background;
                Assert.That(background.Steps.Skip(1).First().Step, Is.EqualTo("And this one"));
            }
        }
    }
}