using mAxCommerce.WindowsPhone.ViewModels;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Ninject
{
    public class ViewModelsModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<AccountCreatePageViewModel>().ToSelf();
            this.Bind<AccountEditPageViewModel>().ToSelf();
            this.Bind<AccountDetailsPageViewModel>().ToSelf();

            this.Bind<AddressCreatePageViewModel>().ToSelf();
            this.Bind<AddressEditPageViewModel>().ToSelf();
            this.Bind<AddressSelectPageViewModel>().ToSelf();

            this.Bind<MainPageViewModel>().ToSelf();
            this.Bind<IRecentProductsViewModel>().To<RecentProductsViewModel>();
            this.Bind<IProductsByCategoryViewModel>().To<ProductsByCategoryViewModel>();

            this.Bind<ChildCategoriesPageViewModel>().ToSelf();
            this.Bind<ProductDetailsPageViewModel>().ToSelf();

            this.Bind<ShoppingCartPageViewModel>().ToSelf();
            this.Bind<OrderConfirmPageViewModel>().ToSelf();
        }
    }
}
