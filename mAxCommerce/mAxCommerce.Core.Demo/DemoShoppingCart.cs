using System.Linq;

namespace mAxCommerce.Core.Demo
{
    public class DemoShoppingCart : DemoProductBasket, IShoppingCart
    {
        public DemoShoppingCart()
        {
            this.Products.AddRange(new DemoProductRepository().GetProducts().Result.Take(6));
        }
    }
}