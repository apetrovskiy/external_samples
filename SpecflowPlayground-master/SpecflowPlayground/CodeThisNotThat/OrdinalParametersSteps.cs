using System.Collections.Generic;
using SpecflowPlayground.RegexSamples;
using TechTalk.SpecFlow;

namespace SpecflowPlayground.CodeThisNotThat
{
    [Binding]
    public class OrdinalParametersSteps
    {
        private List<Product> _products;

        [When(@"I remove the item at index (.*)")]
        public void WhenIRemoveTheItemAtIndex(int index)
        {
            _products.RemoveAt(--index);
        }

    }
}
