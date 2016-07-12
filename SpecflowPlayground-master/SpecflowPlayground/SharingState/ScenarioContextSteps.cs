using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowPlayground.SharingState
{
    [Binding, Scope(Tag = "scenarioContext")]
    public class ScenarioContextSteps
    {
        private const string CalculatorValues = "calculatorValues";
        private const string CalculationResult = "calculationResult";

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int num)
        {
            List<int> values;

            if (!ScenarioContext.Current.ContainsKey(CalculatorValues))
            {
                values = new List<int>();
                ScenarioContext.Current.Add(CalculatorValues, values);
            }
            else
            {
                values = ScenarioContext.Current.Get<List<int>>(CalculatorValues);
            }

            values.Add(num);
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            var values = ScenarioContext.Current.Get<List<int>>(CalculatorValues);
            var calculatorService = new CalculatorService();
            int result = calculatorService.Add(values);

            ScenarioContext.Current.Add(CalculationResult, result);
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            var calculationResult = ScenarioContext.Current.Get<int>(CalculationResult);

            Assert.That(calculationResult, Is.EqualTo(expectedResult));
        }
    }
}
