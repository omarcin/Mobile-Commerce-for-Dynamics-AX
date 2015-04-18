using System;
using Caliburn.Micro;
using mAxCommerce.Core;
using System.Collections.Generic;
using mAxCommerce.WindowsPhone.Services;
using Ninject;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class MainPageViewModel : ConductorViewModelBase.AllActive
    {
        [Inject]
        public IRecentProductsViewModel RecentProducts { get; set; }

        [Inject]
        public IProductsByCategoryViewModel ProductsByCategory { get; set; }

        public void NavigateToAccount()
        {
            this.NavigationService.NavigateLoggedInTo<AccountDetailsPageViewModel>(this.AccountService);
        }
    }
}