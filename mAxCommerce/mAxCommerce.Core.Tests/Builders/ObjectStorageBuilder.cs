using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Tests
{
    public class ObjectStorageBuilder
    {
        private readonly Mock<IObjectStorage> mock;
        private string key;

        public ObjectStorageBuilder()
        {
            this.mock = new Mock<IObjectStorage>();
        }

        public ObjectStorageBuilder SetupDelete()
        {
            this.mock.Setup(m => m.Delete(It.IsAny<string>())).Returns(Task.Delay(0)).Verifiable();
            return this;
        }

        public ObjectStorageBuilder SetupRead(List<Product> products)
        {
            this.mock.Setup(m => m.Read<List<Product>>(It.IsAny<string>()))
                        .Returns(Task.FromResult(products))
                        .Verifiable();
            return this;
        }

        public ObjectStorageBuilder SetupReadThrowsFileNotFoundException()
        {
            this.mock.Setup(m => m.Read<List<Product>>(It.IsAny<string>()))
                .Throws<FileNotFoundException>()
                .Verifiable();
            return this;
        }

        public ObjectStorageBuilder SetupWrite(List<Product> products)
        {
            this.mock.Setup(m => m.Write(It.IsAny<string>(), this.MatchProducts(products))).Returns(Task.Delay(0)).Verifiable();
            return this;
        }

        public IObjectStorage Build()
        {
            return this.mock.Object;
        }

        private List<Product> MatchProducts(List<Product> products)
        {
            return Match.Create<List<Product>>(actual => Enumerable.SequenceEqual(actual.Select(p => p.Id), products.Select(p => p.Id)));
        }
    }
}
