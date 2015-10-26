using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices.AccountManagement;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new PrincipalContext(ContextType.Machine, "server1", "administrator", "Password1");
            var filter = new UserPrincipal(ctx);


            PrincipalSearcher s = new PrincipalSearcher();
            filter.SamAccountName = "Administrator";
            s.QueryFilter = filter;
            var r1 = s.FindAll();

            foreach (var r in r1)
            {
                Console.WriteLine(r.Name);
            }

            Console.ReadLine();
        }
    }
}
