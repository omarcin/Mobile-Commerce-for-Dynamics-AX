using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class ProductsByCategoryViewModel : ViewModelBase, IProductsByCategoryViewModel
    {
        private Category displayCategory;
        private BindableCollection<object> categoriesAndProducts;
        private string displayCategoryId;
        private string displayCategoryName;

        [Inject]
        public IProductRepository ProductRepository { get; set; }

        public BindableCollection<object> CategoriesAndProducts
        {
            get
            {
                return this.categoriesAndProducts;
            }
            set
            {
                this.categoriesAndProducts = value;
                this.NotifyOfPropertyChange(() => CategoriesAndProducts);
            }
        }

        public string DisplayCategoryName
        {
            get
            {
                return this.displayCategoryName;
            }
            set
            {
                this.displayCategoryName = value;
                this.NotifyOfPropertyChange(() => DisplayCategoryName);
            }
        }

        public Category DisplayCategory
        {
            get
            {
                return this.displayCategory;
            }
            set
            {
                this.displayCategory = value;
                this.NotifyOfPropertyChange(() => DisplayCategory);
            }
        }

        public string DisplayCategoryId
        {
            get { return this.displayCategoryId; }
            set
            {
                this.displayCategoryId = value;
                this.NotifyOfPropertyChange(() => DisplayCategoryId);
            }
        }

        public void NavigateToCategory(Category category)
        {
            this.NavigationService.NavigateToCategory(category, parentCategory: this.DisplayCategory);
        }

        public void NavigateToProductDetails(Product product)
        {
            this.NavigationService.NavigateToProductDetails(product);
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();
            await LongRunningJob.Run(this, this.LoadCategoriesAndProducts, "Loading products and categories...");
        }

        private async Task LoadCategoriesAndProducts()
        {
            this.CategoriesAndProducts = new BindableCollection<object>();

            await this.LoadDisplayCategory();

            this.CategoriesAndProducts.AddRange(this.DisplayCategory.ChildCategories);

            IEnumerable<Product> products = await this.ProductRepository.GetProductsByCategoryId(this.DisplayCategory.Id);
            this.CategoriesAndProducts.AddRange(products);
        }

        private async Task LoadDisplayCategory()
        {
            if (this.DisplayCategoryId != null)
            {
                this.DisplayCategory = await this.ProductRepository.GetCategoryById(this.DisplayCategoryId);
            }
            else
            {
                // Load root category
                this.DisplayCategory = await this.ProductRepository.GetCategories();
                this.DisplayCategoryId = this.DisplayCategory.Id;
            }
        }
    }
}
