using mAxCommerce.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class ProductRepositoryMockExtensions
    {
        public static void SetupGetCategoryById(this Mock<IProductRepository> mock, Category category)
        {
            mock.SetupTask(p => p.GetCategoryById(category.Id)).ReturnsResult(category);
        }

        public static void SetupGetProductsByCategoryId(this Mock<IProductRepository> mock, string categoryId, List<Product> products)
        {
            mock.SetupTask(p => p.GetProductsByCategoryId(categoryId)).ReturnsResult(products);
        }

        public static void SetupGetCategories(this Mock<IProductRepository> mock, Category category)
        {
            mock.SetupTask(p => p.GetCategories()).ReturnsResult(category);
        }

        public static void SetupGetProductById(this Mock<IProductRepository> mock, Product product)
        {
            mock.SetupTask(p => p.GetProductById(product.Id)).ReturnsResult(product);
        }
    }
}
