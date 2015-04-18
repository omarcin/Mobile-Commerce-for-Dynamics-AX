using Caliburn.Micro;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModelsStorage
{
    public class ProductDetailsPageViewModelStorage : StorageHandler<ProductDetailsPageViewModel>
    {
        public override void Configure()
        {
            this.Id(vm => vm.ProductId);
            this.Property(vm => vm.IsPopupVisible).InPhoneState();
            this.Property(vm => vm.PopupImageSource).InPhoneState();
            this.Property(vm => vm.ProductId).InPhoneState();
        }
    }
}
