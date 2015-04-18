using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace mAxCommerce.Core.Tests
{
    [TestClass]
    public class ProductRepositoryTests
    {
        [TestMethod]
        public void GetCategoriesReturnsRootCategoryFromServiceClient()
        {
            // Arrange
            WCF.Category category = new CategoryBuilder().BuildWCF();
            IMaxCommerceServiceClient serviceClient = new MaxCommerceServiceClientBuilder()
                                                            .SetupGetCategories(category)
                                                            .Build();

            var sut = new ProductRepository(serviceClient);

            // Act
            Category result = sut.GetCategories().Result;

            // Assert
            serviceClient.AsMock().Verify();
            Assert.AreEqual(category.Id.ToString(), result.Id);
            Assert.AreEqual(category.Name, result.Name);
        }

        [TestMethod]
        public void GetProductsByCategoryIdGetsProductsFromServiceClient()
        {
            // Arrange
            long categoryId = 8472;

            var products = new List<WCF.Product>
            {
                new ProductBuilder().BuildWCF(),
                new ProductBuilder().BuildWCF(),
                new ProductBuilder().BuildWCF()
            };

            var serviceClient = new MaxCommerceServiceClientBuilder()
                                .SetupGetProductsByCategoryId(categoryId, products)
                                .Build();

            var sut = new ProductRepository(serviceClient);

            // Act
            IEnumerable<Product> result = sut.GetProductsByCategoryId(categoryId.ToString()).Result;

            // Assert
            serviceClient.AsMock().Verify();
            CollectionAssert.AreEqual(
                products.Select(p => p.Id.ToString()).ToList(),
                result.Select(r => r.Id).ToList());
        }

        [TestMethod]
        public void GetProductByidGetsProductFromServiceClient()
        {
            // Arrange
            long productId = 984871;
            var product = new ProductBuilder().WithId(productId).BuildWCF();
            var serviceClient = new MaxCommerceServiceClientBuilder()
                                .SetupGetProductById(productId, product)
                                .Build();

            var sut = new ProductRepository(serviceClient);

            // Act
            var result = sut.GetProductById(productId.ToString()).Result;

            // Assert
            serviceClient.AsMock().Verify();
            Assert.AreEqual(productId.ToString(), result.Id);
        }

        [TestMethod]
        public void GetCategoryByIdWhenRootIdPassedReturnsRootCategory()
        {
            // Arrange
            var rootCategory = new CategoryBuilder()
                                .WithChildCategory(new CategoryBuilder().BuildWCF())
                                .WithChildCategory(new CategoryBuilder().BuildWCF())
                                .BuildWCF();

            var serviceClient = new MaxCommerceServiceClientBuilder()
                                    .SetupGetCategories(rootCategory)
                                    .Build();

            var sut = new ProductRepository(serviceClient);

            // Act
            var result = sut.GetCategoryById(rootCategory.Id.ToString()).Result;

            // Assert
            serviceClient.AsMock().Verify();
            Assert.AreEqual(rootCategory.Id.ToString(), result.Id);
        }

        [TestMethod]
        public void GetCategoryByIdWhenChildCategoryIdPassedReturnsCorrectCategory()
        {
            // Arrange
            long childId = 874329832;

            var rootCategory = new CategoryBuilder()
                                .WithChildCategory(new CategoryBuilder()
                                                    .WithChildCategory(new CategoryBuilder().WithId(childId).BuildWCF())
                                                    .BuildWCF())
                                .WithChildCategory(new CategoryBuilder().BuildWCF())
                                .BuildWCF();

            var serviceClient = new MaxCommerceServiceClientBuilder()
                                    .SetupGetCategories(rootCategory)
                                    .Build();

            var sut = new ProductRepository(serviceClient);

            // Act
            var result = sut.GetCategoryById(childId.ToString()).Result;

            // Assert
            serviceClient.AsMock().Verify();
            Assert.AreEqual(childId.ToString(), result.Id);
        }

        [TestMethod]
        public void ProductsImageUrisStartWithServiceBaseAddress()
        {
            // Arrange
            string baseAddress = "http://www.fakewebsitefortesting.com/";
            long imageId = 123532421321;
            var product = new ProductBuilder()
                            .WithImageId(imageId)
                            .BuildWCF();
            var serviceClient = new MaxCommerceServiceClientBuilder()
                                    .SetupServiceBaseAddress(baseAddress)
                                    .SetupGetProductById(product.Id, product)
                                    .Build();

            var sut = new ProductRepository(serviceClient);

            // Act
            Product result = sut.GetProductById(product.Id.ToString()).Result;

            // Assert
            string expectedUri = string.Format("{0}Images?id={1}", baseAddress, imageId);
            serviceClient.AsMock().Verify();
            Assert.AreEqual(expectedUri, result.Images[0].AbsoluteUri);
        }
    }
}
