using System.Collections.Generic;
using System.Runtime.Serialization;

namespace mAxCommerce.WCF
{
    [DataContract]
    public class Category
    {
        [DataMember]
        public long Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<Category> ChildCategories { get; set; }
    }
}
