using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace mAxCommerce.Core
{
    public interface IProductRepository
    {
        Task<Category> GetCategories();

        Task<IEnumerable<Product>> GetProductsByCategoryId(string categoryId);

        Task<Product> GetProductById(string id);

        Task<Category> GetCategoryById(string id);
    }
}
