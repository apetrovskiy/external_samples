using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using DataMigration.Business;


namespace DataMigration.SqlDataAccess
{
    public class ShippersSqlRepository : IShippersRepository
    {
        private readonly NorthwindContext objectContext;

        public ShippersSqlRepository([Configuration]string northwindConnectionString)
        {
            objectContext = new NorthwindContext(northwindConnectionString);
        }

        public IEnumerable<Business.Model.Shipper> GetShippers()
        {
            return from shipper in objectContext.Shippers
                   select new Business.Model.Shipper
                               {
                                   ShipperID = shipper.Shipper_ID,
                                   CompanyName = shipper.Company_Name
                               };

        }

        public void AddShipper(Business.Model.Shipper shipper)
        {
            objectContext.Shippers.AddObject(new Shipper
                                            {
                                                Shipper_ID = shipper.ShipperID,
                                                Company_Name = shipper.CompanyName
                                            });
            objectContext.SaveChanges();
        }
    }
}
