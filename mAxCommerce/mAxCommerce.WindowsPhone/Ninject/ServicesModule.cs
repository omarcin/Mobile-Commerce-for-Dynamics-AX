using mAxCommerce.Core;
using mAxCommerce.Core.Demo;
using mAxCommerce.WindowsPhone.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Ninject
{
    public class ServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IProductRepository>().To<ProductRepository>();
            this.Bind<IMaxCommerceServiceClient>().To<MaxCommerceServicePortableClient>();
            this.Bind<IShoppingCart>().To<ShoppingCart>().InSingletonScope();
            this.Bind<IAccountService>().To<DemoAccountService>();
            this.Bind<IRecentProductsService>().To<RecentProductsService>().InSingletonScope();

        }
    }
}
