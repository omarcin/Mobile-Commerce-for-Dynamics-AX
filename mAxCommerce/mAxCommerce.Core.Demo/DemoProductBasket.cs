using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Demo
{
    public class DemoProductBasket : IProductBasket
    {
        public DemoProductBasket()
        {
            this.Products = new List<Product>();
        }

        protected List<Product> Products { get; set; }

        public virtual Task AddProduct(Product product)
        {
            return Task.Run(() => this.Products.Add(product));
        }

        public virtual Task<IEnumerable<Product>> GetProducts()
        {
            return Task.Run<IEnumerable<Product>>(() => this.Products);
        }

        public Task Clear()
        {
            return Task.Run(() =>
                {
                    this.Products.Clear();
                });
        }
    }
}
