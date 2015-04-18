using Caliburn.Micro;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModelsStorage
{
    public class AccountCreatePageViewModelStorage : StorageHandler<AccountCreatePageViewModel>
    {
        public override void Configure()
        {
            this.Property(vm => vm.AcceptsTerms).InPhoneState();
            this.Property(vm => vm.Email).InPhoneState();
            this.Property(vm => vm.NextPageUriString).InPhoneState();
            this.Property(vm => vm.Password).InPhoneState();
            this.Property(vm => vm.PasswordRepeat).InPhoneState();
        }
    }
}
