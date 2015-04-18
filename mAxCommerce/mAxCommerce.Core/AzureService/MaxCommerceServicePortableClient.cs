using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AzureService = mAxCommerce.WCF;

namespace mAxCommerce.Core
{
    public class MaxCommerceServicePortableClient : IMaxCommerceServiceClient
    {
        private const string BaseAddress = "https://placeholder.servicebus.windows.net/connector/";

        private const string CategoriesUri = "Categories";

        private const string ProductsByCategoryUri = "Categories?id={0}";

        private const string ProductByIdUri = "Products?id={0}";

        private const string OrderUri = "Order";

        private readonly HttpRestClient httpRestClient;

        public MaxCommerceServicePortableClient()
        {
            this.httpRestClient = new HttpRestClient(BaseAddress);
        }

        public string ServiceBaseAddress
        {
            get { return BaseAddress; }
        }

        public async Task<AzureService.Category> GetCategories()
        {
            return await this.httpRestClient.Get<AzureService.Category>(CategoriesUri);
        }

        public async Task<List<AzureService.Product>> GetProductsByCategoryId(long categoryId)
        {
            return await this.httpRestClient.Get<List<AzureService.Product>>(string.Format(ProductsByCategoryUri, categoryId));
        }

        public async Task<AzureService.Product> GetProductById(long productId)
        {
            return await this.httpRestClient.Get<AzureService.Product>(string.Format(ProductByIdUri, productId));
        }

        public async Task<string> CreateOrder(AzureService.Order order)
        {
            return await this.httpRestClient.Post<AzureService.Order, string>(OrderUri, order);
        }

    }
}
