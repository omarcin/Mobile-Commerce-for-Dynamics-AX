using Caliburn.Micro;
using mAxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public interface IRecentProductsViewModel : IViewModel
    {
        BindableCollection<Product> RecentProducts { get; set; }
        void NavigateToProductDetails(Product product);
    }
}
