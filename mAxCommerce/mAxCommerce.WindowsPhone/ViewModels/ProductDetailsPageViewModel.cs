using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using Microsoft.Phone.Controls;
using Ninject;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class ProductDetailsPageViewModel : ViewModelBase
    {
        private Product product;
        private Uri popupImageSource;

        private bool isPopupVisible;
        private string productId;

        [Inject]
        public IProductRepository ProductRepository { get; set; }

        [Inject]
        public IShoppingCart ShoppingCart { get; set; }

        [Inject]
        public IRecentProductsService RecentProductsService { get; set; }

        public Product Product
        {
            get
            {
                return this.product;
            }
            set
            {
                this.product = value;
                this.NotifyOfPropertyChange(() => Product);
                this.NotifyOfPropertyChange(() => CanAddToShoppingCart);
            }
        }

        public string ProductId
        {
            get { return this.productId; } 
            set
            {
                this.productId = value; 
                this.NotifyOfPropertyChange(() => ProductId);
            }
        }

        public Uri PopupImageSource
        {
            get
            {
                return this.popupImageSource;
            }
            set
            {
                this.popupImageSource = value;
                this.NotifyOfPropertyChange(() => PopupImageSource);
            }
        }

        public bool IsPopupVisible
        {
            get
            {
                return this.isPopupVisible;
            }
            set
            {
                this.isPopupVisible = value;
                this.NotifyOfPropertyChange(() => IsPopupVisible);
            }
        }

        public void ShowImagePopup(Uri imageSource)
        {
            this.IsPopupVisible = true;
            this.PopupImageSource = imageSource;
        }

        public bool CanAddToShoppingCart
        {
            get
            {
                return this.Product != null;
            }
        }

        public async void AddToShoppingCart()
        {
            await this.ShoppingCart.AddProduct(this.Product);
            var input = await this.MessageService.ShowCustomMessage("Added to cart.", "Would you like to see your shopping cart?", "Go to cart", "Continue browsing");

            if (input == CustomMessageBoxResult.LeftButton)
            {
                this.NavigationService.UriFor<ShoppingCartPageViewModel>().Navigate();
            }
        }

        public void GoToShoppingCart()
        {
            this.NavigationService.UriFor<ShoppingCartPageViewModel>().Navigate();
        }

        protected override async void OnInitialize()
        {
            base.OnInitialize();
            await LongRunningJob.Run(this, this.LoadData, "Loading...");
        }
  
        private async Task LoadData()
        {
            this.Product = await this.ProductRepository.GetProductById(this.ProductId);
            await this.RecentProductsService.AddProduct(this.Product);
        }
    }
}
