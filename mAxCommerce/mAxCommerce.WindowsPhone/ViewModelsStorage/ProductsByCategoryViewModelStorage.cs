using Caliburn.Micro;
using mAxCommerce.WindowsPhone.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModelsStorage
{
    public class ProductsByCategoryViewModelStorage : StorageHandler<ProductsByCategoryViewModel>
    {
        public override void Configure()
        {
            this.Id(vm => vm.DisplayCategoryId);
            this.Property(vm => vm.DisplayCategoryId).InPhoneState();
            this.Property(vm => vm.DisplayCategoryName).InPhoneState();
        }
    }
}
