using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mAxCommerce.Core.Demo
{
    public class DemoProductRepository : IProductRepository
    {
        private const int ProductsNumber = 25;

        private const int CategoriesNumber = 4;

        private static readonly string[] imagesUrls = new[] {
            "placeholder.jpg",
            "placeholder.jpg",
            "placeholder.jpg",
            "placeholder.jpg",
        };

        private static readonly List<Product> products;

        private static readonly Category rootCategory;

        static DemoProductRepository()
        {
            products = Enumerable.Range(0, ProductsNumber)
                       .Select(CreateFakeProduct)
                       .ToList();

            rootCategory = new Category
            {
                Name = "New collection",
                Id = Guid.NewGuid().ToString(),
                ChildCategories = Enumerable.Range(0, CategoriesNumber)
                .Select(CreateFakeCategory)
                .ToList()
            };
        }

        private static Product CreateFakeProduct(int i)
        {
            return new Product
            {
                Id = i.ToString(),
                Name = "Product " + i,
                Description = "Lorem ipsum dolor sit amet",
                Price = 9.99m,
                Images = new List<Uri>
                {
                    new Uri(imagesUrls[i % imagesUrls.Length], UriKind.Absolute),
                    new Uri(imagesUrls[(i + 1) % imagesUrls.Length], UriKind.Absolute),
                    new Uri(imagesUrls[(i + 2) % imagesUrls.Length], UriKind.Absolute),
                    new Uri(imagesUrls[(i + 1) % imagesUrls.Length], UriKind.Absolute)
                }
            };
        }
        
        private static Category CreateFakeCategory(int i)
        {
            return new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Category " + i
            };
        }

        public Task<IEnumerable<Product>> GetProducts()
        {
            return Task.FromResult<IEnumerable<Product>>(products);
        }

        public Task<Product> GetProductById(string productId)
        {
            return Task.Run(() => products.First(product => product.Id == productId));
        }

        public Task<Category> GetCategories()
        {
            return Task.FromResult<Category>(rootCategory);
        }

        public Task<IEnumerable<Product>> GetProductsByCategoryId(string categoryId)
        {
            return this.GetProducts();
        }

        public async Task<Category> GetCategoryById(string id)
        {
            Category rootCategory = await this.GetCategories();

            IEnumerable<Category> allCategories = this.TraverseCategoryHierarchy(rootCategory);

            return allCategories.Single(c => c.Id == id);
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
