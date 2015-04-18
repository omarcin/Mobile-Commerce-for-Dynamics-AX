using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class AccountDetailsPageViewModel : AccountPageViewModelBase
    {
        public void CreateAddress()
        {
            this.NavigationService.UriFor<AddressCreatePageViewModel>().Navigate();
        }

        public void LogOut()
        {
            this.AccountService.LogOut();
            this.NavigationService.GoBackOrDefault();
        }

        public void Edit()
        {
            this.NavigationService.UriFor<AccountEditPageViewModel>().Navigate();
        }

        public void EditAddress(Address address)
        {
            this.NavigationService.UriFor<AddressEditPageViewModel>()
                .WithParam(vm => vm.AddressId, address.Id)
                .Navigate();
        }

        protected override Account CreateAccount()
        {
            return this.AccountService.CurrentAccount;
        }

        protected override Account RefreshAccount(Account oldAccount)
        {
            return this.AccountService.CurrentAccount;
        }
    }
}
