using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace NCT
{
    public interface IEmployeeManager
    {
        System.Int32? EmployeeID { get; set; }
        System.String FirstName { get; set; }
        System.String LastName { get; set; }
    }
}
