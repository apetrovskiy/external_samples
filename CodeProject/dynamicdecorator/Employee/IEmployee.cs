using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace ThirdPartyHR
{
    public interface IEmployee
    {
        System.Int32? EmployeeID { get; set; }
        System.String FirstName { get; set; }
        System.String LastName { get; set; }
        System.DateTime DateOfBirth { get; set; }
        System.String FullName();
        System.Single Salary();
    }
}
