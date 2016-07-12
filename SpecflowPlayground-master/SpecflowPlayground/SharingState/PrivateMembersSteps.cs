using System.Collections.Generic;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace SpecflowPlayground.SharingState
{
    [Binding, Scope(Tag = "privateMembers")]
    public class PrivateMembersSteps
    {
        private readonly List<int> _values = new List<int>();
        private int _result;

        [Given(@"I have entered (.*) into the calculator")]
        public void GivenIHaveEnteredIntoTheCalculator(int num)
        {
            _values.Add(num);
        }

        [When(@"I press add")]
        public void WhenIPressAdd()
        {
            var calculatorService = new CalculatorService();
            _result = calculatorService.Add(_values);
        }

        [Then(@"the result should be (.*) on the screen")]
        public void ThenTheResultShouldBeOnTheScreen(int expectedResult)
        {
            Assert.That(_result, Is.EqualTo(expectedResult));
        }
    }
}
