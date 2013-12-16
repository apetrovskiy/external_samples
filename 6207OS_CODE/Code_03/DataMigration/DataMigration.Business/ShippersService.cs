using DataMigration.Business.Model;

namespace DataMigration.Business
{
    public class ShippersService
    {
        private readonly IShippersRepository sourceRepository;
        private readonly IShippersRepository targetRepository;

public ShippersService([Source]IShippersRepository sourceRepository, [Target]IShippersRepository targetRepository)
        {
            this.sourceRepository = sourceRepository;
            this.targetRepository = targetRepository;
        }

        //public ShippersService(IShippersRepository sourceRepository, IShippersRepository targetRepository)
        //{
        //    this.sourceRepository = sourceRepository;
        //    this.targetRepository = targetRepository;
        //}

        public void MigrateShippers()
        {

            foreach (Shipper shipper in sourceRepository.GetShippers())
            {
                targetRepository.AddShipper(shipper);
            }
        }
    }
}
