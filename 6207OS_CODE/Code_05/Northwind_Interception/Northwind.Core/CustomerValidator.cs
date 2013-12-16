using System;

namespace Northwind.Core
{
public class CustomerValidator : ICustomerValidator
{
    private readonly ICustomerRepository repository;

    public CustomerValidator(ICustomerRepository repository)
    {
        if (repository == null)
        {
            throw new ArgumentNullException("repository");
        }
        this.repository = repository;
    }

    public bool ValidateUniqueness(string customerID)
    {
        return repository.Get(customerID) == null;
    }
}
}
