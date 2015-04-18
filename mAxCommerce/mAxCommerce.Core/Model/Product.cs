using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.Core
{
    public class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<Uri> Images { get; set; }

        public string Id { get; set; }
    }
}
