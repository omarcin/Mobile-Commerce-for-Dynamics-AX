using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Demo
{
    public class DemoSlowProductRepository : IProductRepository
    {
        private const int DefaultSleepTimeInSeconds = 1;

        private readonly IProductRepository productRepository = new DemoProductRepository();

        public async Task<Product> GetProductById(string value)
        {
            await this.Sleep();
            return await this.productRepository.GetProductById(value);
        }

        public async Task<Category> GetCategories()
        {
            await this.Sleep();
            return await this.productRepository.GetCategories();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(string categoryId)
        {
            await this.Sleep();
            return await this.productRepository.GetProductsByCategoryId(categoryId);
        }

        public async Task<Category> GetCategoryById(string id)
        {
            await this.Sleep();
            return await this.productRepository.GetCategoryById(id);
        }

        private Task Sleep(int seconds = DefaultSleepTimeInSeconds)
        {
            return Task.Delay(TimeSpan.FromSeconds(seconds));
        }
    }
}
