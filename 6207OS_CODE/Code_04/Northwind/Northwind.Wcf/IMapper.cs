using System.Collections.Generic;
using Northwind.Core;


namespace Northwind.Wcf
{
public interface IMapper
{
    Customer Map(CustomerContract customer);

    CustomerContract Map(Customer customer);

    IEnumerable<CustomerContract> Map(IEnumerable<Customer> customers);
}
}
