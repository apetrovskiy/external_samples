using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowPlayground.CodeThisNotThat
{
    [Binding]
    public class ParsingTableFieldsSteps
    {
        private Customer _customer;

        [Given(@"the customer")]
        public void GivenTheCustomer(Table table)
        {
            Customer customer = table.CreateInstance<Customer>();

            string[] addressParts = table.Rows[0]["Address"].Split(';');

            Address address = new Address();
            address.Line1 = addressParts[0];
            address.Line2 = addressParts[1];
            address.City = addressParts[2];
            address.State = addressParts[3];
            address.Zipcode = addressParts[4];

            customer.Address = address;

            _customer = customer;
        }

        [Given(@"the customer \(fixed\)")]
        public void GivenTheCustomerFixed(Table table)
        {
            _customer = table.CreateInstance<Customer>();
        }

        [Given(@"the address")]
        public void GivenTheAddress(Table table)
        {
            _customer.Address = table.CreateInstance<Address>();
        }

    }
}
