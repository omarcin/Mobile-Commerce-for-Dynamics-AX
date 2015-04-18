using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using Ninject;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class RecentProductsViewModel : ViewModelBase, IRecentProductsViewModel
    {
        private BindableCollection<Product> recentProducts;

        [Inject]
        public IRecentProductsService RecentProductsService { get; set; }

        public BindableCollection<Product> RecentProducts
        {
            get
            {
                return this.recentProducts;
            }
            set
            {
                this.recentProducts = value;
                this.NotifyOfPropertyChange(() => RecentProducts);
            }
        }

        public void NavigateToProductDetails(Product product)
        {
            this.NavigationService.NavigateToProductDetails(product);
        }

        protected async override void OnActivate()
        {
            base.OnActivate();
            await LongRunningJob.Run(this, this.LoadRecentProducts, "Loading recent products...");
        }

        private async Task LoadRecentProducts()
        {
            IEnumerable<Product> recentProducts = await this.RecentProductsService.GetProducts();
            this.RecentProducts = new BindableCollection<Product>(recentProducts);
        }
    }
}

