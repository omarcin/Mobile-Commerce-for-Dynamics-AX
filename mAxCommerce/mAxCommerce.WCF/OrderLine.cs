using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace mAxCommerce.WCF
{
    [DataContract]
    public class OrderLine
    {
        [DataMember]
        public long ProductId { get; set; }

        [DataMember]
        public decimal Quantity { get; set; }
    }
}
