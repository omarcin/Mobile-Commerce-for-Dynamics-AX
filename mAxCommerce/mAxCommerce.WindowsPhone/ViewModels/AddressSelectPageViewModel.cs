using Caliburn.Micro;
using mAxCommerce.Core;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class AddressSelectPageViewModel : ViewModelBase
    {
        public BindableCollection<Address> DeliveryAddresses { get; private set; }

        public void SelectAddress(Address address)
        {
            this.NavigationService.UriFor<OrderConfirmPageViewModel>()
                .WithParam(vm => vm.DeliveryAddressId, address.Id)
                .Navigate();
        }

        public void CreateAddress()
        {
            this.NavigationService.UriFor<AddressCreatePageViewModel>().Navigate();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            this.DeliveryAddresses = new BindableCollection<Address>(this.AccountService.CurrentAccount.DeliveryAddresses);
            this.Refresh();
        }
    }
}
