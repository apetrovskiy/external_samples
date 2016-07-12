using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowPlayground.SharingState
{
    [Binding, Scope(Tag = "contextObject")]
    public class ContextObjectSteps
    {
        private readonly CalculatorContext _calculatorContext;

        public ContextObjectSteps(CalculatorContext calculatorContext)
        {
            _calculatorContext = calculatorContext;
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int num)
        {
            _calculatorContext.Values.Add(num);
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            var calculatorService = new CalculatorService();
            _calculatorContext.Result = calculatorService.Add(_calculatorContext.Values);
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            Assert.That(_calculatorContext.Result, Is.EqualTo(expectedResult));
        }
    }

    public class CalculatorContext
    {
        public CalculatorContext()
        {
            Values = new List<int>();
        }

        public List<int> Values { get; set; }
        public int Result { get; set; }
    }
}
