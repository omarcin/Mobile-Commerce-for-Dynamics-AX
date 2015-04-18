using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AzureService = mAxCommerce.WCF;

namespace mAxCommerce.Core
{
    public interface IMaxCommerceServiceClient
    {
        Task<AzureService.Category> GetCategories();

        Task<List<AzureService.Product>> GetProductsByCategoryId(long categoryId);

        Task<AzureService.Product> GetProductById(long productId);

        Task<string> CreateOrder(AzureService.Order order);

        string ServiceBaseAddress { get; }
    }
}
