using System.Collections.Generic;
using DataMigration.Business.Model;

namespace DataMigration.Business
{
    public interface IShippersRepository
    {
        IEnumerable<Shipper> GetShippers();

        void AddShipper(Shipper shipper);
    }
}
