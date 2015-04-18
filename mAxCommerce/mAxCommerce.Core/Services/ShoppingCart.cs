using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.Core
{
    public class ShoppingCart : ProductBasket, IShoppingCart
    {
        private const string Key = "ShoppingCart";

        public ShoppingCart(IObjectStorage objectStorage)
            : base(objectStorage)
        {
        }

        protected override string StorageKey
        {
            get { return Key; }
        }

        protected override IEnumerable<Product> AddProductToCurrentProducts(IEnumerable<Product> currentProducts, Product product)
        {
            return Enumerable.Concat(currentProducts, new[] { product });
        }
    }
}
