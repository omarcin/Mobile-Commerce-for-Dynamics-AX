using Caliburn.Micro;
using mAxCommerce.Core;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class ShoppingCartPageViewModelBase : ViewModelBase
    {
        public ShoppingCartPageViewModelBase()
        {
            this.Products = new BindableCollection<Product>();
        }

        [Inject]
        public IShoppingCart ShoppingCart { get; set; }

        public BindableCollection<Product> Products { get; private set; }

        public decimal TotalAmount { get; private set; }

        protected override async void OnActivate()
        {
            base.OnActivate();
            IEnumerable<Product> products = await this.ShoppingCart.GetProducts();
            this.Products = new BindableCollection<Product>(products);
            this.TotalAmount = this.Products.Sum(product => product.Price);
            this.Refresh();
        }
    }
}
