namespace Northwind.Core
{
    public interface ICustomerValidator
    {
        bool ValidateUniqueness(string customerID);

    }
}
