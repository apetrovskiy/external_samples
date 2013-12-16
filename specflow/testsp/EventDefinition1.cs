/*
 * Created by SharpDevelop.
 * User: Alexander
 * Date: 12/12/2012
 * Time: 9:16 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using TechTalk.SpecFlow;

namespace testsp
{
    [Binding]
    public class EventDefinition1
    {
        [BeforeStep]
        public void BeforeStep()
        {
            // TODO: implement logic that has to run before each scenario step
            // For storing and retrieving scenario-specific data, 
            // the instance fields of the class or the
            //     ScenarioContext.Current
            // collection can be used.
            // For storing and retrieving feature-specific data, the 
            //     FeatureContext.Current
            // collection can be used.
            // Use the attribute overload to specify tags. If tags are specified, the event 
            // handler will be executed only if any of the tags are specified for the 
            // feature or the scenario.
            //     [BeforeStep("mytag")]
        }

        [AfterStep]
        public void AfterStep()
        {
            // TODO: implement logic that has to run after each scenario step
            Console.WriteLine("AfterStep");
        }

        [BeforeScenarioBlock]
        public void BeforeScenarioBlock()
        {
            // TODO: implement logic that has to run before each scenario block (given-when-then)
            Console.WriteLine("BeforeScenarioBlock");
        }

        [AfterScenarioBlock]
        public void AfterScenarioBlock()
        {
            // TODO: implement logic that has to run after each scenario block (given-when-then)
            Console.WriteLine("AfterScenarioBlock");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            // TODO: implement logic that has to run before executing each scenario
            Console.WriteLine("BeforeScenario");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            // TODO: implement logic that has to run after executing each scenario
            Console.WriteLine("AfterScenario");
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            // TODO: implement logic that has to run before executing each feature
            Console.WriteLine("BeforeFeature");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            // TODO: implement logic that has to run after executing each feature
            Console.WriteLine("AfterFeature");
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            // TODO: implement logic that has to run before the entire test run
            Console.WriteLine("BeforeTestRun");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            // TODO: implement logic that has to run after the entire test run
            Console.WriteLine("AfterTestRun");
        }
    }
}
