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
    public class SharingStateBetweenBindingsSteps
    {
        private ServiceProxy _serviceProxy = new ServiceProxy();
        private Customer _customer;
        private Response _response;

        [Given(@"the new customer")]
        public void GivenTheNewCustomer(Table table)
        {
            _customer = table.CreateInstance<Customer>();
        }

        [Given(@"the customer's address")]
        public void GivenTheCustomerSAddress(Table table)
        {
            _customer.Address = table.CreateInstance<Address>();
        }

        [When(@"I save the customer")]
        public void WhenISaveTheCustomer()
        {
            _response = _serviceProxy.Post(_customer);
        }

        [Then(@"a successful response should be returned")]
        public void ThenASuccessfulResponseShouldBeReturned()
        {
            Assert.That(_response.IsSuccessful, Is.True);
        }

//        [Given(@"the new customer")]
//        public void GivenTheNewCustomer(Table table)
//        {
//            var customer = table.CreateInstance<Customer>();
//            ScenarioContext.Current.Add("customer", customer);
//        }
//
//        [Given(@"the customer's address")]
//        public void GivenTheCustomerSAddress(Table table)
//        {
//            var customer = ScenarioContext.Current.Get<Customer>("customer");
//            customer.Address = table.CreateInstance<Address>();
//        }
//
//        [When(@"I save the customer")]
//        public void WhenISaveTheCustomer()
//        {
//            var customer = ScenarioContext.Current.Get<Customer>("customer");
//            var response = _serviceProxy.Post(customer);
//
//            ScenarioContext.Current.Add("saveCustomerResponse", response);
//        }
//
//        [Then(@"a successful response should be returned")]
//        public void ThenASuccessfulResponseShouldBeReturned()
//        {
//            var response = ScenarioContext.Current.Get<Response>("saveCustomerResponse");
//
//            Assert.That(response.IsSuccessful, Is.True);
//        }
    }
}
