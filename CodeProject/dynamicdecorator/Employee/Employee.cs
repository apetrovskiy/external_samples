using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace ThirdPartyHR
{
    public class Employee : IEmployee
    {
        #region Properties

        public System.Int32? EmployeeID { get; set; }
        public System.String FirstName { get; set; }
        public System.String LastName { get; set; }
        public System.DateTime DateOfBirth { get; set; }

        #endregion

        public Employee(
            System.Int32? employeeid
            , System.String firstname
            , System.String lastname
            , System.DateTime bDay
        )
        {
            this.EmployeeID = employeeid;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.DateOfBirth = bDay;
        }

        public Employee() { }

        public System.String FullName()
        {
            System.String s = FirstName + " " + LastName;
            Console.WriteLine("Full Name: " + s);
            return s;
        }

        public System.Single Salary()
        {
            System.Single i = 10000.12f;
            Console.WriteLine("Salary: " + i);
            return i;
        }
    }
}
