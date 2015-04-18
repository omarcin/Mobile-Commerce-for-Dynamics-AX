using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Demo
{
    public class DemoMaxCommerceServiceClient : IMaxCommerceServiceClient
    {
        private static WCF.Category rootCategory;
        private static List<WCF.Product> products;

        static DemoMaxCommerceServiceClient()
        {
            rootCategory = new WCF.Category
            {
                Id = 100,
                Name = "Root category",
                ChildCategories = (from i in Enumerable.Range(0, 6)
                                  select new WCF.Category
                                  {
                                      Id = i,
                                      Name = "Child category " + i,
                                      ChildCategories = new List<WCF.Category>()
                                  }).ToList()
            };

            products = (from i in Enumerable.Range(0, 20)
                        select new WCF.Product
                        {
                            Id = i,
                            ImageIds = new List<long>(),
                            Name = "Fake product " + i,
                            Price = 9.99M,
                            Description = "Lorem ipsum lorem ipsum lorem ipsum"
                        }).ToList();
        }

        public Task<WCF.Category> GetCategories()
        {
            return Task.FromResult(rootCategory);
        }

        public Task<List<WCF.Product>> GetProductsByCategoryId(long categoryId)
        {
            return Task.FromResult(products);
        }

        public Task<WCF.Product> GetProductById(long productId)
        {
            return Task.FromResult(products.Single(p => p.Id == productId));
        }

        public Task<string> CreateOrder(WCF.Order order)
        {
            return Task.FromResult("FAKE-000123");
        }

        public string ServiceBaseAddress
        {
            get { return string.Empty; }
        }
    }
}
