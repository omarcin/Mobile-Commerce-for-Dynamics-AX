using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class AccountEditPageViewModel : AccountPageViewModelBase
    {
        protected override Account CreateAccount()
        {
            return this.AccountService.CurrentAccount;
        }

        public void Save()
        {
            this.AccountService.UpdateAccount(this.Account);
            this.NavigationService.GoBackOrDefault();
        }
    }
}