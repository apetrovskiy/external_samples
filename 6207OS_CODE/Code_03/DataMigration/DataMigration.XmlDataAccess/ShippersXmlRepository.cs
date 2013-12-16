using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using DataMigration.Business;
using DataMigration.Business.Model;

namespace DataMigration.XmlDataAccess
{
    public class ShippersXmlRepository : IShippersRepository
    {
        private readonly string documentPath;

        public ShippersXmlRepository([Configuration]string xmlRepositoryPath)
        {
            this.documentPath = xmlRepositoryPath;
        }

        public IEnumerable<Shipper> GetShippers()
        {
            var document = XDocument.Load(documentPath);
            return from e in document.Elements("Shipper")
                   select new Shipper
                               {
                                   ShipperID = Convert.ToInt32(e.Element("ShipperID").Value),
                                   CompanyName = e.Element("CompanyName").Value
                               };
        }

        public void AddShipper(Shipper shipper)
        {
            var document = XDocument.Load(documentPath);
            document.Root.Add(new XElement("Shipper",
                new XElement("ShipperID", shipper.ShipperID),
                new XElement("CompanyName", shipper.CompanyName)));
            document.Save(documentPath);
        }
    }
}
