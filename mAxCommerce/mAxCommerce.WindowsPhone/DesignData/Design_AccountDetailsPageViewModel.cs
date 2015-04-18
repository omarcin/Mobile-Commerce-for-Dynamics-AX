using System;
using System.Linq;
using mAxCommerce.Core.Demo;
using mAxCommerce.WindowsPhone.ViewModels;
using Caliburn.Micro;
using mAxCommerce.Core;

namespace mAxCommerce.WindowsPhone.DesignData
{
    public class Design_AccountDetailsPageViewModel
    {
        public Account Account { get; set; }

        public Design_AccountDetailsPageViewModel()
        {
            this.Account = new DemoAccountService().CurrentAccount;
            this.DeliveryAddresses = new BindableCollection<Address>(this.Account.DeliveryAddresses);
            this.SelectedAddress = this.DeliveryAddresses.First();
            this.Email = "email@sampledata.com";
        }

        public BindableCollection<Address> DeliveryAddresses { get; set; }

        public Address SelectedAddress { get; set; }

        public string Email { get; set; }
    }
}