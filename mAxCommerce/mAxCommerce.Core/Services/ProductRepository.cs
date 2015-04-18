using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using AzureService = mAxCommerce.WCF;

namespace mAxCommerce.Core
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMaxCommerceServiceClient serviceClient;

        public ProductRepository(IMaxCommerceServiceClient serviceClient)
        {
            this.serviceClient = serviceClient;
        }

        public async Task<Category> GetCategories()
        {
            AzureService.Category rootCategory = await this.serviceClient.GetCategories();
            return this.MapCategory(rootCategory);
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(string categoryId)
        {
            long id = long.Parse(categoryId);
            List<AzureService.Product> products = await this.serviceClient.GetProductsByCategoryId(id);
            return from p in products select this.MapProduct(p);
        }

        public async Task<Product> GetProductById(string value)
        {
            long productId = long.Parse(value);
            AzureService.Product product = await this.serviceClient.GetProductById(productId);
            return this.MapProduct(product);
        }

        public async Task<Category> GetCategoryById(string id)
        {
            Category rootCategory = await this.GetCategories();

            IEnumerable<Category> allCategories = this.TraverseCategoryHierarchy(rootCategory);

            return allCategories.Single(c => c.Id == id);
        }

        private Category MapCategory(AzureService.Category category)
        {
            return new Category
            {
                Id = category.Id.ToString(),
                Name = category.Name,
                ChildCategories = category.ChildCategories.Select(this.MapCategory).ToList()
            };
        }

        private Product MapProduct(AzureService.Product product)
        {
            return new Product
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Images = product.ImageIds.Select(this.UriForImage).ToList()
            };
        }

        private Uri UriForImage(long imageId)
        {
            string uriString = string.Format("{0}Images?id={1}", this.serviceClient.ServiceBaseAddress, imageId);
            return new Uri(uriString, UriKind.Absolute);
        }

        private IEnumerable<Category> TraverseCategoryHierarchy(Category category)
        {
            yield return category;

            IEnumerable<Category> subcategories = category.ChildCategories.SelectMany(this.TraverseCategoryHierarchy);

            foreach (Category subcategory in subcategories)
            {
                yield return subcategory;
            }
        }
    }
}
