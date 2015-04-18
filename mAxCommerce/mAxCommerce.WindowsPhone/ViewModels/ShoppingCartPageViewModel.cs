using mAxCommerce.WindowsPhone.Services;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class ShoppingCartPageViewModel : ShoppingCartPageViewModelBase
    {
        public bool CanCheckout
        {
            get
            {
                return this.Products.Any();
            }
        }

        public void Checkout()
        {
            this.NavigationService.NavigateLoggedInTo<AddressSelectPageViewModel>(this.AccountService);
        }
    }
}
