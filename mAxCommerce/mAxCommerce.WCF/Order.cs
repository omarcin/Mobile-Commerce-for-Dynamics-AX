using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WCF
{
    [DataContract]
    public class Order
    {
        public Order()
        {
            this.Lines = new List<OrderLine>();
        }

        [DataMember]
        public List<OrderLine> Lines { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string CountryISOCode { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string ZipCode { get; set; }
    }
}
