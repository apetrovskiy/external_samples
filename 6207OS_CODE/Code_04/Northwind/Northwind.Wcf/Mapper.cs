using System.Collections.Generic;
using System.Linq;
using Northwind.Core;

namespace Northwind.Wcf
{
    public class Mapper : IMapper
    {
        public Customer Map(CustomerContract contract)
        {
            return new Customer
                       {
                           ID = contract.ID,
                           City = contract.City,
                           CompanyName = contract.CompanyName,
                           Phone = contract.Phone,
                           PostalCode = contract.PostalCode
                       };
        }

        public CustomerContract Map(Customer customer)
        {
            return new CustomerContract
            {
                ID = customer.ID,
                City = customer.City,
                CompanyName = customer.CompanyName,
                Phone = customer.Phone,
                PostalCode = customer.PostalCode
            };
        }

        public IEnumerable<CustomerContract> Map(IEnumerable<Customer> customers)
        {
            return customers.Select(Map);
        }
    }
}
