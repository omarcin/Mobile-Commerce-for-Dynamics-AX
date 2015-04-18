using Caliburn.Micro;
using mAxCommerce.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public interface IProductsByCategoryViewModel : IViewModel
    {
        BindableCollection<object> CategoriesAndProducts { get; set; }
        string DisplayCategoryName { get; set; }
        Category DisplayCategory { get; set; }
        string DisplayCategoryId { get; set; }
        void NavigateToCategory(Category category);
        void NavigateToProductDetails(Product product);
    }
}
