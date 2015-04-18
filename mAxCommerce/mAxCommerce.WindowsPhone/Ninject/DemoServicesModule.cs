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
    public class DemoServicesModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IProductRepository>().To<DemoProductRepository>();
            this.Bind<IMaxCommerceServiceClient>().To<DemoMaxCommerceServiceClient>();
            this.Bind<IShoppingCart>().To<DemoShoppingCart>().InSingletonScope();
            this.Bind<IAccountService>().To<DemoAccountService>();
            this.Bind<IRecentProductsService>().To<DemoRecentProductsService>().InSingletonScope();
        }
    }
}
