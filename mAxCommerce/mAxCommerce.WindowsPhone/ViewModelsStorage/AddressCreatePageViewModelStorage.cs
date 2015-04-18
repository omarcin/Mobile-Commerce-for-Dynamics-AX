using Caliburn.Micro;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModelsStorage
{
    public class AddressCreatePageViewModelStorage : StorageHandler<AddressCreatePageViewModel>
    {
        public override void Configure()
        {
            this.Property(vm => vm.City).InPhoneState();
            this.Property(vm => vm.Country).InPhoneState();
            this.Property(vm => vm.FirstName).InPhoneState();
            this.Property(vm => vm.LastName).InPhoneState();
            this.Property(vm => vm.Number).InPhoneState();
            this.Property(vm => vm.PostalCode).InPhoneState();
            this.Property(vm => vm.Street).InPhoneState();
        }
    }
}
