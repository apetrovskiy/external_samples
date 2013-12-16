using System;
using System.ComponentModel.DataAnnotations;

namespace Northwind.Core
{
public class UniqueCustomerIdAttribute : ValidationAttribute
{
    [Inject]
    public ICustomerValidator Validator { get; set; }

    public override bool IsValid(object value)
    {
        if (Validator == null)
        {
            throw new Exception("Validator is not specified.");
        }
        if (string.IsNullOrEmpty(value as string))
        {
            return false;
        }
        return Validator.ValidateUniqueness(value as string);
    }
}
}