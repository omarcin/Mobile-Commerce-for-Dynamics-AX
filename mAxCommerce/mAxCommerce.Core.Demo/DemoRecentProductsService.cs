using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Demo
{
    public class DemoRecentProductsService : DemoProductBasket, IRecentProductsService
    {
        private const int Limit = 6;

        public override async Task<IEnumerable<Product>> GetProducts()
        {
            if (!this.Products.Any())
            {
                await this.Initialize();
            }
            return await base.GetProducts();
        }

        public override Task AddProduct(Product product)
        {
            return Task.Run(() =>
                {
                    if (this.Products.Any(p => p.Id == product.Id))
                    {
                        return;
                    }

                    IEnumerable<Product> oldProducts = this.Products.Take(Limit - 1);
                    this.Products = new List<Product> { product };
                    this.Products.AddRange(oldProducts);
                });
        }

        private async Task Initialize()
        {
            IEnumerable<Product> demoProducts = await new DemoProductRepository().GetProducts();
            this.Products.AddRange(demoProducts.Skip(10).Take(5));
        }
    }
}
