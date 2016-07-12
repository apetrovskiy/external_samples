using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowPlayground.SharingState
{
    [Binding, Scope(Tag = "domainObject")]
    public class DomainObjectSteps
    {
        private readonly Calculator _calculator;

        public DomainObjectSteps(Calculator calculator)
        {
            _calculator = calculator;
        }

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int num)
        {
            _calculator.EnterNumber(num);
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            _calculator.Add();
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            Assert.That(expectedResult, Is.EqualTo(_calculator.Result));
        }
    }

    public class Calculator
    {
        private readonly List<int> _values = new List<int>();
        private int _result;

        public void EnterNumber(int num)
        {
            _values.Add(num);
        }

        public void Add()
        {
            var calculatorService = new CalculatorService();
            _result = calculatorService.Add(_values);
        }

        public int Result { get { return _result; } }
    }
}
