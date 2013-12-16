using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Northwind.Wcf
{
[ServiceContract]
public interface ICustomerService
{
    [OperationContract]
    IEnumerable<CustomerContract> GetAllCustomers();

    [OperationContract]
    void AddCustomer(CustomerContract customer);
}
}
