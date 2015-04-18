using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core
{
    public abstract class ProductBasket : IProductBasket
    {
        private readonly IObjectStorage objectStorage;

        protected ProductBasket(IObjectStorage objectStorage)
        {
            this.objectStorage = objectStorage;
        }

        public virtual async Task AddProduct(Product product)
        {
            IEnumerable<Product> currentProducts = await this.GetProducts();
            IEnumerable<Product> newProducts = this.AddProductToCurrentProducts(currentProducts, product);
            await this.objectStorage.Write(this.StorageKey, newProducts.ToList());
        }

        public virtual async Task<IEnumerable<Product>> GetProducts()
        {
            try
            {
                return await this.objectStorage.Read<List<Product>>(this.StorageKey);
            }
            catch (FileNotFoundException exception)
            {
                Debug.WriteLine(exception);
                Task.WaitAll(this.objectStorage.Write(this.StorageKey, new List<Product>()));
                return Enumerable.Empty<Product>();
            }
        }

        public async Task Clear()
        {
            await this.objectStorage.Delete(this.StorageKey);
        }

        protected abstract string StorageKey { get; }

        protected abstract IEnumerable<Product> AddProductToCurrentProducts(IEnumerable<Product> currentProducts, Product product);
    }
}
