using System.ComponentModel.DataAnnotations;

namespace Northwind.Core
{
    public class Customer
    {
        [Required, UniqueCustomerId]
        public string ID { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        [StringLength(10)]
        public string Phone { get; set; }
    }


}
