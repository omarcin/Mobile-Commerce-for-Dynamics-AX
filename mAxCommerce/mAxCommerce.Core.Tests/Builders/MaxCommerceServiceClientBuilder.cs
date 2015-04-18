using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Tests
{
    public class MaxCommerceServiceClientBuilder
    {
        private Mock<IMaxCommerceServiceClient> mock;

        public MaxCommerceServiceClientBuilder()
        {
            this.mock = new Mock<IMaxCommerceServiceClient>();
            this.mock.SetupGet(m => m.ServiceBaseAddress).Returns("http://www.fakewebsite.com/");
        }

        public MaxCommerceServiceClientBuilder SetupServiceBaseAddress(string baseAddress)
        {
            this.mock.SetupGet(m => m.ServiceBaseAddress).Returns(baseAddress).Verifiable();
            return this;
        }

        public MaxCommerceServiceClientBuilder SetupGetCategories(WCF.Category category)
        {
            this.mock.Setup(m => m.GetCategories()).Returns(Task.FromResult(category)).Verifiable();
            return this;
        }

        public MaxCommerceServiceClientBuilder SetupGetProductsByCategoryId(long categoryId, List<WCF.Product> products)
        {
            this.mock.Setup(m => m.GetProductsByCategoryId(categoryId)).Returns(Task.FromResult(products)).Verifiable();
            return this;
        }

        public MaxCommerceServiceClientBuilder SetupGetProductById(long productId, WCF.Product product)
        {
            this.mock.Setup(m => m.GetProductById(productId)).Returns(Task.FromResult(product)).Verifiable();
            return this;
        }

        public IMaxCommerceServiceClient Build()
        {
            return this.mock.Object;
        }
    }
}
