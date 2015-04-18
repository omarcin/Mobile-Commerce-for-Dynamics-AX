using mAxCommerce.Core;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using System.Windows;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class OrderConfirmPageViewModel : ShoppingCartPageViewModelBase
    {
        private Address deliveryAddress;

        [Inject]
        public IMaxCommerceServiceClient MaxCommerceServiceClient { get; set; }

        public Address DeliveryAddress
        {
            get
            {
                return this.deliveryAddress;
            }
            private set
            {
                this.deliveryAddress = value;
                this.NotifyOfPropertyChange(() => DeliveryAddress);
                this.NotifyOfPropertyChange(() => DeliveryAddressId);
            }
        }

        public string DeliveryAddressId
        {
            get
            {
                if (this.deliveryAddress == null)
                {
                    return null;
                }
                else
                {
                    return this.deliveryAddress.Id;
                }
            }
            set
            {
                this.DeliveryAddress = this.AccountService.CurrentAccount.DeliveryAddresses.Single(address => address.Id == value);
            }
        }

        public async void PlaceOrder()
        {
            await LongRunningJob.Run(this, this.CreateOrder, "Placing order...");
        }

        private async Task CreateOrder()
        {
            List<WCF.OrderLine> lines = (from product in this.Products
                                         let productId = long.Parse(product.Id)
                                         select new WCF.OrderLine
                                                {
                                                    ProductId = productId,
                                                    Quantity = 1
                                                })
                                         .ToList();

            WCF.Order order = new WCF.Order
            {
                Lines = lines,
                FirstName = this.DeliveryAddress.FirstName,
                LastName = this.DeliveryAddress.LastName,
                Street = this.DeliveryAddress.Street + " " + this.DeliveryAddress.Number,
                City = this.DeliveryAddress.City,
                ZipCode = this.DeliveryAddress.PostalCode,
                CountryISOCode = "PL"
            };

            string orderNumber = await this.MaxCommerceServiceClient.CreateOrder(order);

            await this.ShoppingCart.Clear();
            this.Products.Clear();

            this.MessageService.ShowMessage("Order placed", "Order number: " + orderNumber, MessageBoxButton.OK);

            this.NavigationService.UriFor<MainPageViewModel>().Navigate();
        }
    }
}
