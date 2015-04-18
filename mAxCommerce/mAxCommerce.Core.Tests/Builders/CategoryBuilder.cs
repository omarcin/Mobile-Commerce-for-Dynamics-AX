using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Tests
{
    public class CategoryBuilder
    {
        private static readonly Random random = new Random();
        private string name;
        private long id;
        private List<WCF.Category> childCategories;

        public CategoryBuilder()
        {
            this.name = "Default category name";
            this.id = random.Next();
            this.childCategories = new List<WCF.Category>();
        }

        public CategoryBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public CategoryBuilder WithId(long id)
        {
            this.id = id;
            return this;
        }

        public CategoryBuilder WithChildCategory(WCF.Category childCategory)
        {
            this.childCategories.Add(childCategory);
            return this;
        }

        public WCF.Category BuildWCF()
        {
            return new WCF.Category
            {
                Name = this.name,
                Id = this.id,
                ChildCategories = this.childCategories
            };
        }
    }
}
