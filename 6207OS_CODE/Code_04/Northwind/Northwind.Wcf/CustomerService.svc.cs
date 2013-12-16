using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Northwind.Core;

namespace Northwind.Wcf
{
public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository repository;
    private readonly IMapper mapper;

    public CustomerService(ICustomerRepository repository, IMapper mapper)
    {
        if (repository == null)
        {
            throw new ArgumentNullException("repository");
        }
        if (mapper == null)
        {
            throw new ArgumentNullException("mapper");
        }
        this.repository = repository;
        this.mapper = mapper;
    }

    public IEnumerable<CustomerContract> GetAllCustomers()
    {
        var customers = repository.GetAll();
        return mapper.Map(customers);
    }

    public void AddCustomer(CustomerContract customer)
    {
        repository.Add(mapper.Map(customer));
    }
}
}
