using Caliburn.Micro;
using mAxCommerce.WindowsPhone.ViewModelsStorage;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Ninject
{
    public class StorageHandlersModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IStorageHandler>().To<AccountCreatePageViewModelStorage>();
            this.Bind<IStorageHandler>().To<AccountEditPageViewModelStorage>();
            this.Bind<IStorageHandler>().To<AddressCreatePageViewModelStorage>();
            this.Bind<IStorageHandler>().To<AddressEditPageViewModelStorage>();
            this.Bind<IStorageHandler>().To<ProductDetailsPageViewModelStorage>();
            this.Bind<IStorageHandler>().To<ChildCategoriesPageViewModelStorage>();
            this.Bind<IStorageHandler>().To<ProductsByCategoryViewModelStorage>();
        }
    }
}
