using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.Core
{
    public class Category
    {
        public Category()
        {
            this.ChildCategories = new List<Category>();
        }

        public string Name { get; set; }

        public List<Category> ChildCategories { get; set; }

        public string Id { get; set; }
    }
}
