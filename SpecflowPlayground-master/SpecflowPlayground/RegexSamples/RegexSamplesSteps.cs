using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowPlayground.RegexSamples
{
    [Binding]
    public class Transforms
    {
        [StepArgumentTransformation(@"from (A-Z|Z-A)")]
        public SortOrder SortOrderTransform(string sortOrderPhrase)
        {
            if (sortOrderPhrase == "Z-A")
                return SortOrder.Descending;

            return SortOrder.Ascending;
        }
    }

    [Binding]
    public class RegexSamplesSteps
    {
        private List<Product> _products;

        [Given(@"the following devices?")]
        public void GivenTheFollowingDevices(Table table)
        {
            _products = table.CreateSet<Product>().ToList();
        }

        [When(@"I sort by product name (.*)")]
        [When(@"I sort by product name (ascending|descending)")]
        public void WhenISortByProductName(SortOrder sortOrder)
        {
            _products.Sort((s1, s2) =>
            {
                if (sortOrder == SortOrder.Descending)
                    return s1.ProductName.CompareTo(s2.ProductName) * (-1);

                return s1.ProductName.CompareTo(s2.ProductName);
            });

            int i = 1;
            _products.ForEach(p => p.Index = i++);
        }

        [Then(@"they should be in the following order")]
        public void ThenTheyShouldBeInTheFollowingOrder(Table table)
        {
            table.CompareToSet(_products);
        }

        [When(@"(?:I\s)?remove the (\d+)(?:st|nd|rd|th) item")]
        public void WhenIRemoveTheItem(int index)
        {
            _products.RemoveAt(--index);
        }

        [Then(@"the following devices remain")]
        public void ThenTheFollowingDevicesRemain(Table table)
        {
            table.CompareToSet(_products);
        }

    }

    public class Product
    {
        public string ProductName { get; set; }
        public int Index { get; set; }
    }

    public enum SortOrder
    {
        Ascending = 0,
        Descending = 1
    }
}
