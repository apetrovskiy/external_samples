using System.Collections.Generic;

namespace Northwind.Core
{
public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();

    Customer Get(string customerID);

    void Add(Customer customer);
}
}
