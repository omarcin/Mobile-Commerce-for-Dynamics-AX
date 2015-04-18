using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace mAxCommerce.WCF
{
    [ServiceContract]
    public interface IMaxCommerceService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/Categories", ResponseFormat = WebMessageFormat.Json)]
        Category GetCategories();

        [OperationContract]
        [WebGet(UriTemplate = "/Categories?id={categoryId}", ResponseFormat = WebMessageFormat.Json)]
        List<Product> GetProductsByCategory(long categoryId);

        [OperationContract]
        [WebGet(UriTemplate = "/Products?id={productId}", ResponseFormat = WebMessageFormat.Json)]
        Product GetProductById(long productId);

        [OperationContract]
        [WebGet(UriTemplate = "/Images?id={imageId}")]
        Stream GetProductImage(long imageId);

        [OperationContract]
        [WebInvoke(UriTemplate = "/Order", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CreateOrder(Order order);
    }
}
