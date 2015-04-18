using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Tests
{
    public class ProductBuilder
    {
        private static readonly Random random = new Random();
        private long id;
        private string name;
        private string description;
        private decimal price;
        private List<long> imageIds;
        private List<Uri> imageUris;

        public ProductBuilder()
        {
            this.name = "Default product name";
            this.id = random.Next();
            this.description = "Default description";
            this.price = 1;
            this.imageIds = new List<long>();
            this.imageUris = new List<Uri>();
        }

        public ProductBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ProductBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public ProductBuilder WithPrice(decimal price)
        {
            this.price = price;
            return this;
        }

        public ProductBuilder WithId(long id)
        {
            this.id = id;
            return this;
        }

        public ProductBuilder WithId(string id)
        {
            this.id = long.Parse(id);
            return this;
        }
            

        public ProductBuilder WithImageId(long imageId)
        {
            this.imageIds.Add(imageId);
            this.imageUris.Add(new Uri("www.fakeuri.com/Images?id=" + imageId, UriKind.RelativeOrAbsolute));
            return this;
        }

        public WCF.Product BuildWCF()
        {
            return new WCF.Product
            {
                Id = this.id,
                Name = this.name,
                Description = this.description,
                Price = this.price,
                ImageIds = this.imageIds
            };
        }

        public Product Build()
        {
            return new Product
            {
                Id = this.id.ToString(),
                Name = this.name,
                Description = this.description,
                Price = this.price,
                Images = this.imageUris
            };
        }
    }
}
