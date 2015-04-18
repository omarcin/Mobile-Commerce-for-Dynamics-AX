using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mAxCommerce.Core
{
    public interface IProductBasket
    {
        Task AddProduct(Product product);
        Task<IEnumerable<Product>> GetProducts();
        Task Clear();
    }
}
