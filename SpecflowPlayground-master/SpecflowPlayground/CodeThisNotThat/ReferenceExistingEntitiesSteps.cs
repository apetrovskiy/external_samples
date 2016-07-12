using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowPlayground.CodeThisNotThat
{
    [Binding]
    public class ReferenceExistingEntitiesSteps
    {
        private int _selectedProductId;
        private Dictionary<string, int> _productIdLookup;

        [When(@"I choose the product with ID (.*)")]
        public void WhenIChooseTheProductWithID(int productId)
        {
            _selectedProductId = productId;
        }

        [Given(@"I have the following products")]
        public void GivenIHaveTheFollowingProducts(Table table)
        {
            _productIdLookup = new Dictionary<string, int>();

            foreach (var row in table.Rows)
                _productIdLookup.Add(row["Name"], row.GetInt32("Id"));
        }

        [When(@"I select the (.*)")]
        public void WhenISelectThe(string productName)
        {
            if (!_productIdLookup.ContainsKey(productName))
                Assert.Fail("The product {0} does not exist in the look up dictionary.", productName);

            _selectedProductId = _productIdLookup[productName];
        }

    }
}
