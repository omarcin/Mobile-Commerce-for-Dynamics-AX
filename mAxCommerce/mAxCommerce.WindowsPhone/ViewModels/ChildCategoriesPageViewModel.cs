using Caliburn.Micro;
using mAxCommerce.Core;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class ChildCategoriesPageViewModel : ConductorViewModelBase.OneActive<IProductsByCategoryViewModel>
    {
        public string DisplayCategoryId { get; set; }
        
        public string ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        [Inject]
        public IProductRepository ProductRepository { get; set; }

        protected override async void OnInitialize()
        {
            base.OnInitialize();

            await LongRunningJob.Run(this, this.LoadCategories, "Loading categories...");
        }

        private async Task LoadCategories()
        {
            this.ParentCategory = await this.ProductRepository.GetCategoryById(this.ParentCategoryId);

            List<IProductsByCategoryViewModel> viewModels = this.CreateChildViewModels();

            this.Items.AddRange(viewModels);

            this.ActiveItem = this.Items.Single(vm => vm.DisplayCategoryId == this.DisplayCategoryId);
        }

        private List<IProductsByCategoryViewModel> CreateChildViewModels()
        {
            return this.ParentCategory.ChildCategories
                                      .Select(this.CreateProductsByCategoryViewModel)
                                      .ToList();
        }

        private IProductsByCategoryViewModel CreateProductsByCategoryViewModel(Category category)
        {
            var viewModel = IoC.Get<IProductsByCategoryViewModel>();
            viewModel.DisplayCategoryId = category.Id;
            viewModel.DisplayCategoryName = category.Name;
            return viewModel;
        }
    }
}