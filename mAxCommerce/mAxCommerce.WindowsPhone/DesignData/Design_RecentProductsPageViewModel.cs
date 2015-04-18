using System.Linq;
using Caliburn.Micro;
using mAxCommerce.Core.Demo;
using mAxCommerce.WindowsPhone.ViewModels;
using mAxCommerce.Core;

namespace mAxCommerce.WindowsPhone.DesignData
{
    public class Design_RecentProductsPageViewModel : RecentProductsViewModel
    {
        public Design_RecentProductsPageViewModel()
        {
            var repository = new DemoProductRepository();
            var products = repository.GetProducts().Result;
            this.RecentProducts = new BindableCollection<Product>(products);
        }
   }
}