using Caliburn.Micro;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModelsStorage
{
    public class AccountEditPageViewModelStorage : StorageHandler<AccountEditPageViewModel>
    {
        public override void Configure()
        {
            this.Property(vm => vm.Email).InPhoneState();
        }
    }
}
