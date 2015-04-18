using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Tests
{
    [TestClass]
    public class RecentProductsServiceTests
    {
        [TestMethod]
        public void ClearCallsObjectStorageDelete()
        {
            // Arrange
            var objectStorage = new ObjectStorageBuilder()
                                    .SetupDelete()
                                    .Build();

            var sut = new RecentProductsService(objectStorage);

            // Act
            sut.Clear().Wait();
            
            // Assert
            objectStorage.AsMock().Verify();
        }

        [TestMethod]
        public void AddProductWhenNoProductsWritesTheProductToStorage()
        {
            // Data
            var currentProducts = new List<Product>();
            var productToAdd = new ProductBuilder().Build();
            var expectedProducts = new List<Product> { productToAdd };

            // Test
            this.TestAddProduct(currentProducts, productToAdd, expectedProducts);
        }

        [TestMethod]
        public void AddProductWhenSixProductsRemovesLastProduct()
        {
            // Data
            var currentProducts = Enumerable.Range(0, 6)
                                            .Select(_ => new ProductBuilder().Build())
                                            .ToList();

            var productToAdd = new ProductBuilder().Build();
            var expectedProducts = new List<Product> 
                { 
                    productToAdd, 
                    currentProducts[0],
                    currentProducts[1],
                    currentProducts[2],
                    currentProducts[3],
                    currentProducts[4]
                };

            // Test
            this.TestAddProduct(currentProducts, productToAdd, expectedProducts);
        }

        [TestMethod]
        public void AddProductWhenFiveProductsAddsTheProductToFront()
        {
            // Data
            var currentProducts = Enumerable.Range(0, 5)
                                            .Select(_ => new ProductBuilder().Build())
                                            .ToList();

            var productToAdd = new ProductBuilder().Build();
            var expectedProducts = new List<Product> 
                { 
                    productToAdd, 
                    currentProducts[0],
                    currentProducts[1],
                    currentProducts[2],
                    currentProducts[3],
                    currentProducts[4]
                };

            // Test
            this.TestAddProduct(currentProducts, productToAdd, expectedProducts);
        }

        [TestMethod]
        public void AddProductWhenAddingSameProductDoesNotModifyCollection()
        {
            // Data
            var currentProducts = Enumerable.Range(0, 4)
                                            .Select(_ => new ProductBuilder().Build())
                                            .ToList();

            var productToAdd = new ProductBuilder()
                                    .WithId(currentProducts[2].Id)
                                    .Build();

            var expectedProducts = currentProducts;

            // Test
            this.TestAddProduct(currentProducts, productToAdd, expectedProducts);
        }

        [TestMethod]
        public void GetProductReadsProductsFromStorage()
        {
            // Arrange
            var products = Enumerable.Range(0, 4)
                                    .Select(_ => new ProductBuilder().Build())
                                    .ToList();
            var objectStorage = new ObjectStorageBuilder()
                                    .SetupRead(products)
                                    .Build();

            var sut = new RecentProductsService(objectStorage);

            // Act
            var result = sut.GetProducts().Result;

            // Assert
            objectStorage.AsMock().Verify();

            CollectionAssert.AreEqual(
                products.Select(p => p.Id).ToList(),
                result.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void GetProductWhenStorageThrowsExceptionStoresAndReturnsEmptyCollection()
        {
            // Arrange
            var objectStorage = new ObjectStorageBuilder()
                                    .SetupReadThrowsFileNotFoundException()
                                    .SetupWrite(new List<Product>())
                                    .Build();

            var sut = new RecentProductsService(objectStorage);

            // Act
            var result = sut.GetProducts().Result;

            // Assert
            objectStorage.AsMock().Verify();

            CollectionAssert.AreEqual(
                new List<Product>(),
                result.ToList());
        }

        private void TestAddProduct(List<Product> currentProducts, Product productToAdd, List<Product> expectedProducts)
        {
            // Arrange
            var objectStorage = new ObjectStorageBuilder()
                                    .SetupRead(currentProducts)
                                    .SetupWrite(expectedProducts)
                                    .Build();

            var sut = new RecentProductsService(objectStorage);

            // Act
            sut.AddProduct(productToAdd).Wait();

            // Assert
            objectStorage.AsMock().Verify();
        }       
    }
}
