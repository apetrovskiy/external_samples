using System.Runtime.Serialization;

namespace Northwind.Wcf
{
[DataContract]
public class CustomerContract
{
    [DataMember]
    public string ID { get; set; }

    [DataMember]
    public string CompanyName { get; set; }

    [DataMember]
    public string City { get; set; }

    [DataMember]
    public string PostalCode { get; set; }

    [DataMember]
    public string Phone { get; set; }
}
}