using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core
{
    public class RecentProductsService : ProductBasket, IRecentProductsService
    {
        private const string Key = "RecentProductsService";

        private const int Limit = 6;

        public RecentProductsService(IObjectStorage objectStorage)
            : base(objectStorage)
        {
        }

        protected override string StorageKey
        {
            get { return Key; }
        }

        protected override IEnumerable<Product> AddProductToCurrentProducts(IEnumerable<Product> currentProducts, Product product)
        {
            if (currentProducts.Any(p => p.Id == product.Id))
            {
                return currentProducts;
            }

            IEnumerable<Product> oldProducts = currentProducts.Take(Limit - 1);
            return Enumerable.Concat(new[] { product }, oldProducts);
        }
    }
}
