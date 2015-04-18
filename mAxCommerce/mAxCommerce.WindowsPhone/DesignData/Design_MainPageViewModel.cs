using System;
using System.Linq;
using mAxCommerce.Core.Demo;
using mAxCommerce.WindowsPhone.ViewModels;
using Caliburn.Micro;
using mAxCommerce.Core;

namespace mAxCommerce.WindowsPhone.DesignData
{
    public class Design_MainPageViewModel : MainPageViewModel
    {
        public  Design_MainPageViewModel()
        {
            this.ProductsByCategory = new Design_ProductsByCategoryPageViewModel();
            this.RecentProducts = new Design_RecentProductsPageViewModel();
        }
   }
}