using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Northwind.Core;

namespace Northwind.SqlDataAccess
{
    public class SqlCustomerRepository : ICustomerRepository
    {
        private readonly Mapper mapper;
        private readonly NorthwindEntities context;

        public SqlCustomerRepository(Mapper mapper, NorthwindEntities context)
        {
            if (mapper == null)
            {
                throw new ArgumentNullException("mapper");
            }
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.mapper = mapper;
            this.context = context;
        }

        public IEnumerable<Core.Customer> GetAll()
        {
            return mapper.Map(context.Customers);
        }

        public Core.Customer Get(string customerID)
        {
            var customer = context.Customers
                .SingleOrDefault(c => c.Customer_ID == customerID);

            return mapper.Map(customer);
        }

        public void Add(Core.Customer domainCustomer)
        {
            var customer = mapper.Map(domainCustomer);
            context.Customers.AddObject(customer);
            context.SaveChanges();
        }
    }
}
