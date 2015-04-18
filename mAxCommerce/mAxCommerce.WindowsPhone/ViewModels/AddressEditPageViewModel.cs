using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class AddressEditPageViewModel : AddressPageViewModelBase
    {
        public string AddressId
        {
            get
            {
                return this.Address.Id;
            }
            set
            {
                this.Address = this.AccountService.CurrentAccount.DeliveryAddresses.Single(a => a.Id == value);
                this.Refresh();
            }
        }

        public void Save()
        {
            Account account = this.AccountService.CurrentAccount;
            int index = this.GetOldAddressIndex(account);

            account.DeliveryAddresses[index] = this.Address;

            this.AccountService.UpdateAccount(account);
            this.NavigationService.GoBackOrDefault();
        }

        public void Delete()
        {
            Account account = this.AccountService.CurrentAccount;
            int index = this.GetOldAddressIndex(account);

            account.DeliveryAddresses.RemoveAt(index);

            this.AccountService.UpdateAccount(account);
            this.NavigationService.GoBackOrDefault();
        }

        private int GetOldAddressIndex(Account account)
        {
            Address oldAddress = account.DeliveryAddresses.Single(a => a.Id == this.Address.Id);
            return account.DeliveryAddresses.IndexOf(oldAddress);
        }
    }
}
