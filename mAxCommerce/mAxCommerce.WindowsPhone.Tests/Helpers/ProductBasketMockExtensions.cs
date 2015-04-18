using mAxCommerce.Core;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Tests.Helpers
{
    public static class ProductBasketMockExtensions
    {
        public static void SetupGetProducts<TMock>(this Mock<TMock> mock, List<Product> products) where TMock : class, IProductBasket
        {
            mock.SetupTask(pb => pb.GetProducts()).ReturnsResult(products);
        }

        public static void SetupAddProduct<TMock>(this Mock<TMock> mock, Product product) where TMock : class, IProductBasket
        {
            mock.SetupTask(sc => sc.AddProduct(It.Is<Product>(p => p.Id == product.Id)));
        }
    }
}
