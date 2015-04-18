using mAxCommerce.WCF.Aif;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace mAxCommerce.WCF
{
    public class MaxCommerceService : IMaxCommerceService
    {
        private const string UserName = "";

        private readonly mAxCommerceService aifServiceClient;
        private readonly CallContext callContext;

        public MaxCommerceService()
            : this(new mAxCommerceServiceClient())
        {
        }

        public MaxCommerceService(mAxCommerceService aifServiceClient)
        {
            this.aifServiceClient = aifServiceClient;
            this.callContext = new CallContext
            {
                Company = "CEU",
                LogonAsUser = UserName
            };
        }

        public Category GetCategories()
        {
            var request = new mAxCommerceServiceGetCategoriesRequest(this.callContext);
            mAxCommerceCategory category = this.aifServiceClient.getCategories(request).response;
            return this.MapCategory(category);
        }

        public List<Product> GetProductsByCategory(long categoryId)
        {
            var request = new mAxCommerceServiceGetProductsByCategoryRequest(this.callContext, categoryId);
            mAxCommerceProduct[] product = this.aifServiceClient.getProductsByCategory(request).response;
            return product.Select(this.MapProduct).ToList();
        }

        public Product GetProductById(long productId)
        {
            var request = new mAxCommerceServiceGetProductByIdRequest(this.callContext, productId);
            mAxCommerceProduct product = this.aifServiceClient.getProductById(request).response;
            return this.MapProduct(product);
        }

        public string CreateOrder(Order order)
        {
            mAxCommerceOrder maxCommerceOrder = this.MapOrder(order);
            var request = new mAxCommerceServiceCreateOrderRequest(this.callContext, maxCommerceOrder);
            return this.aifServiceClient.createOrder(request).response;
        }

        public Stream GetProductImage(long imageId)
        {
            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            }

            var request = new mAxCommerceServiceGetProductImageBase64Request(this.callContext, imageId);
            string imageBase64 = this.aifServiceClient.getProductImageBase64(request).response;
            byte[] imageBytes = Convert.FromBase64String(imageBase64);
            MemoryStream stream = new MemoryStream(imageBytes);
            return stream;
        }

        private Category MapCategory(mAxCommerceCategory category)
        {
            return new Category
            {
                Id = category.parmId,
                Name = category.parmName,
                ChildCategories = category.parmChildCategories.Select(this.MapCategory).ToList()
            };
        }

        private Product MapProduct(mAxCommerceProduct product)
        {
            return new Product
            {
                Id = product.parmId,
                Name = product.parmName,
                Description = product.parmDescription,
                Price = product.parmPrice,
                ImageIds = product.parmImages.ToList()
            };
        }

        private mAxCommerceOrder MapOrder(Order order)
        {
            return new mAxCommerceOrder
            {
                parmLines = order.Lines.Select(this.MapOrderLine).ToArray(),
                parmCity = order.City,
                parmCountry = order.CountryISOCode,
                parmFirstName = order.FirstName,
                parmLastName = order.LastName,
                parmStreet = order.Street,
                parmZipCode = order.ZipCode
            };
        }

        private mAxCommerceOrderLine MapOrderLine(OrderLine orderLine)
        {
            return new mAxCommerceOrderLine
            {
                parmProductId = orderLine.ProductId,
                parmQty = orderLine.Quantity
            };
        }
    }
}
