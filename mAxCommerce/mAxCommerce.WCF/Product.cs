using System.Collections.Generic;
using System.Runtime.Serialization;

namespace mAxCommerce.WCF
{
    [DataContract]
    public class Product
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public List<long> ImageIds { get; set; }
    }
}
