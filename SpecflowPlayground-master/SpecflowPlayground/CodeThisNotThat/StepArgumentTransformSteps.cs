using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SpecflowPlayground.RegexSamples;
using TechTalk.SpecFlow;

namespace SpecflowPlayground.CodeThisNotThat
{
    [Binding]
    public class StepArgumentTransformSteps
    {
        private List<Product> _products;

        [StepArgumentTransformation(@"from (A-Z|Z-A)")]
        public SortOrder SortOrderTransform(string sortOrderPhrase)
        {
            if (sortOrderPhrase == "Z-A")
                return SortOrder.Descending;

            return SortOrder.Ascending; 
        }

        [When(@"the products are sorted by name (.*)")]
        public void WhenTheProductsAreSortedByName(SortOrder sortOrder)
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

//        [When(@"the products are sorted by name (.*)")]
//        public void WhenTheProductsAreSortedByName(string sortOrderText)
//        {
//            SortOrder sortOrder = SortOrder.Ascending;
//
//            if (sortOrderText == "from A-Z")
//                sortOrder = SortOrder.Ascending;
//            else if (sortOrderText == "from Z-A")
//                sortOrder = SortOrder.Descending;
//            else
//                Assert.Fail("Invalid sort order text.");
//
//            _products.Sort((s1, s2) =>
//            {
//                if (sortOrder == SortOrder.Descending)
//                    return s1.ProductName.CompareTo(s2.ProductName) * (-1);
//
//                return s1.ProductName.CompareTo(s2.ProductName);
//            });
//
//            int i = 1;
//            _products.ForEach(p => p.Index = i++); 
//        }

    }
}
