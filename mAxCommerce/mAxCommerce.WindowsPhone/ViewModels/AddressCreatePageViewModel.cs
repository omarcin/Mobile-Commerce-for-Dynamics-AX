using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class AddressCreatePageViewModel : AddressPageViewModelBase
    {
        public void CreateAddress()
        {
            Account account = this.AccountService.CurrentAccount;
            account.DeliveryAddresses.Add(this.Address);
            
            this.AccountService.UpdateAccount(account);
            
            this.NavigationService.GoBackOrDefault();
        }

        private void LoadDefaults()
        {
            Address existingAddress = this.AccountService.CurrentAccount.DeliveryAddresses.LastOrDefault();
            if (existingAddress != null)
            {
                this.Address.FirstName = existingAddress.FirstName;
                this.Address.LastName = existingAddress.LastName;
                this.Refresh();
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            this.LoadDefaults();
        }
    }
}
