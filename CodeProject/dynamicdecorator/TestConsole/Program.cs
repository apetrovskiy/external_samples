using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Proxies;

using ThirdPartyHR;

namespace NCT
{
    class DynamicDecoratorTest
    {
        static void Main(string[] args)
        {
            IEmployee em = new Employee(1, "John", "Smith", new DateTime(1990, 4, 1));
            IEmployee tpCheckRight = (IEmployee)ObjectProxyFactory.CreateProxy(em,
                new String[] { "Salary" },
                new Decoration(new DecorationDelegate(UserRightCheck), null), null);

            IEmployee tpLogCheckRight = (IEmployee)ObjectProxyFactory.CreateProxy(tpCheckRight,
                new String[] { "Salary", "FullName" },
                null, new Decoration(new DecorationDelegate(ExitLog), null));

            //IEmployee dLog2CheckRight = (IEmployee)ObjectProxyFactory.CreateProxy(dLogCheckRight,
            //    new String[] { "Salary", "FullName" },
            //    new Decoration(new DecorationDelegate(EnterLog), null), null);

            try
            {
                tpLogCheckRight.FullName();
                Console.WriteLine("");
                tpLogCheckRight.Salary();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            //IEmployee em = new Employee(1, "John", "Doe", new DateTime(1990, 4, 1));
            //IEmployee dCheckRight = new EmployeeDecoratorCheckRight(em);
            //IEmployee dLogCheckRight = new EmployeeDecoratorLog(dCheckRight);

            //dLogCheckRight.FullName();
            //Console.WriteLine("");
            //dLogCheckRight.Salary();

            Console.Read();
        }

        private static void UserRightCheck(object target, object[] parameters)
        {
            Console.WriteLine("Do security check here");
            //if (parameters != null && parameters[0].ToString().ToUpper() == "SUPERUSER")
            //    return;
            //else
            //    throw new Exception("No right to call Salary!");
        }

        private static void ExitLog(object target, object[] parameters)
        {
            Console.WriteLine("Do exit log here");
        }

        private static void EnterLog(object target, object[] parameters)
        {
            Console.WriteLine("Do enter log here");
        }
    }
}
