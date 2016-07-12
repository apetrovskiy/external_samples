using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowPlayground.CodeThisNotThat
{
    [Binding]
    public class TableManipulationSteps
    {
        private Address _address;

//        [Given(@"the following address")]
//        public void GivenTheFollowingAddress(Table table)
//        {
//            Address address = table.CreateInstance<Address>();
//        }

        [Given(@"the following address")]
        public void GivenTheFollowingAddress(Table table)
        {
            Address address = new Address();

            address.Line1 = table.Rows[0]["Line 1"];
            address.Line2 = table.Rows[0]["Line 2"];
            address.City = table.Rows[0]["City"];
            address.State = table.Rows[0]["State"];
            address.Zipcode = table.Rows[0]["Zipcode"];
        }

//        [Then(@"the following address should be returned by the service")]
//        public void ThenTheFollowingAddressShouldBeReturnedByTheService(Table table)
//        {
//            table.CompareToInstance(_address);
//        }

        [Then(@"the following address should be returned by the service")]
        public void ThenTheFollowingAddressShouldBeReturnedByTheService(Table table)
        {
            Assert.AreEqual(table.Rows[0]["Line 1"], _address.Line1);
            Assert.AreEqual(table.Rows[0]["Line 2"], _address.Line2);
            Assert.AreEqual(table.Rows[0]["City"], _address.City);
            Assert.AreEqual(table.Rows[0]["State"], _address.State);
            Assert.AreEqual(table.Rows[0]["Zipcode"], _address.Zipcode);
        }


    }
}
