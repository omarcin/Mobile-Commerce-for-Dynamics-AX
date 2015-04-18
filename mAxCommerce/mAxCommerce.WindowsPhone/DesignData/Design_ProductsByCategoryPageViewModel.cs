using System.Linq;
using Caliburn.Micro;
using mAxCommerce.Core.Demo;
using mAxCommerce.WindowsPhone.ViewModels;

namespace mAxCommerce.WindowsPhone.DesignData
{
    public class Design_ProductsByCategoryPageViewModel : ProductsByCategoryViewModel
    {
        public Design_ProductsByCategoryPageViewModel()
        {
            var repository = new DemoProductRepository();
            var products = repository.GetProducts().Result;
            var category = repository.GetCategories().Result;
            this.CategoriesAndProducts = new BindableCollection<object>(category.ChildCategories.Union<object>(products));

            this.DisplayCategory = category;
        }
   }
}