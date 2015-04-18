using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class OrderConfirmPageViewModelTests : ViewModelTestBase<OrderConfirmPageViewModel>
    {
        private Account account;
        private List<Product> products;
        private Mock<IShoppingCart> shoppingCart;
        private Mock<IMaxCommerceServiceClient> serviceClient;

        [TestInitialize]
        public void TestInitialize()
        {
            this.account = TestData.Accounts.CreateAccount();
            this.accountService.SetupCurrentAccount(this.account);

            this.products = TestData.Products.CreateProducts();
            this.shoppingCart = new Mock<IShoppingCart>();
            this.shoppingCart.SetupTask(s => s.GetProducts()).ReturnsResult(this.products);

            this.serviceClient = new Mock<IMaxCommerceServiceClient>();

            this.viewModel.ShoppingCart = this.shoppingCart.Object;
            this.viewModel.MaxCommerceServiceClient = this.serviceClient.Object;

            this.viewModel.Activate();
        }

        [TestMethod]
        public void ProductsAreEqualToShoppingCartProducts()
        {
            // Assert
            CollectionAssert.AreEqual(
                this.products.Select(p => p.Id).ToList(),
                this.viewModel.Products.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void SettingDeliveryAddressIdSetsDeliveryAddress()
        {
            // Arrange
            var address = this.account.DeliveryAddresses[3];

            // Act
            this.viewModel.DeliveryAddressId = address.Id;
            string deliveryAddressId = this.viewModel.DeliveryAddress.Id;

            // Assert
            Assert.AreEqual(address.Id, deliveryAddressId);
        }

        [TestMethod]
        public void TotalAmountEqualsSumOfProductPrices()
        {
            // Arrange
            decimal sum = this.products.Sum(p => p.Price);

            // Act
            decimal totalAmount = this.viewModel.TotalAmount;

            // Assert
            Assert.AreEqual(sum, totalAmount);
        }

        [TestMethod]
        public void PlaceOrderClearsShoppingCart()
        {
            // Arrange
            this.viewModel.DeliveryAddressId = this.account.DeliveryAddresses[0].Id;
            this.serviceClient.SetupTask(s => s.CreateOrder(It.IsAny<WCF.Order>())).ReturnsResult(TestData.GetRandomString());
            this.shoppingCart.SetupTask(s => s.Clear());

            // Act
            this.viewModel.PlaceOrder();

            // Assert
            this.shoppingCart.VerifyAll();
        }

        [TestMethod]
        public void PlaceOrderClearsProducts()
        {
            // Arrange
            this.viewModel.DeliveryAddressId = this.account.DeliveryAddresses[0].Id;
            this.serviceClient.SetupTask(s => s.CreateOrder(It.IsAny<WCF.Order>())).ReturnsResult(TestData.GetRandomString());

            // Act
            this.viewModel.PlaceOrder();

            // Assert
            Assert.IsFalse(this.viewModel.Products.Any());
        }

        [TestMethod]
        public void PlaceOrderShowsMessageWithOrderNumber()
        {
            this.viewModel.DeliveryAddressId = this.account.DeliveryAddresses[0].Id;
            string orderNumber = TestData.GetRandomString();
            this.serviceClient.SetupTask(s => s.CreateOrder(It.IsAny<WCF.Order>())).ReturnsResult(orderNumber);
            
            this.messageService.Setup(
                s => s.ShowMessage(
                    It.IsAny<string>(),
                    It.Is<string>(m => m.Contains(orderNumber)),
                    MessageBoxButton.OK));

            // Act
            this.viewModel.PlaceOrder();

            // Assert
            this.messageService.VerifyAll();
        }

        [TestMethod]
        public void PlaceOrderNavigatesToMainPage()
        {
            // Arrange
            this.viewModel.DeliveryAddressId = this.account.DeliveryAddresses[0].Id;
            this.serviceClient.SetupTask(s => s.CreateOrder(It.IsAny<WCF.Order>())).ReturnsResult(TestData.GetRandomString());

            this.navigationService.SetupNavigateTo<MainPageViewModel>();

            // Act
            this.viewModel.PlaceOrder();

            // Assert
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void PlaceOrderPassessCorrectOrderToServiceClient()
        {
            this.viewModel.DeliveryAddressId = this.account.DeliveryAddresses[1].Id;
            this.serviceClient.SetupTask(s => s.CreateOrder(Match.Create<WCF.Order>(this.ValidateOrder))).ReturnsResult(TestData.GetRandomString());

            // Act
            this.viewModel.PlaceOrder();

            // Assert
            this.serviceClient.VerifyAll();
        }

        private bool ValidateOrder(WCF.Order order)
        {
            List<string> orderLinesIds = order.Lines.Select(l => l.ProductId.ToString()).ToList();
            List<string> productsIds = this.viewModel.Products.Select(p => p.Id).ToList();
            Address address = this.viewModel.DeliveryAddress;

            return order.City == address.City &&
                    order.CountryISOCode == address.Country &&
                    order.FirstName == address.FirstName &&
                    order.LastName == address.LastName &&
                    order.Street == address.Street + " " + address.Number &&
                    order.ZipCode == address.PostalCode &&
                    order.Lines.All(line => line.Quantity == 1) &&
                    orderLinesIds.Count == productsIds.Count &&
                    productsIds.All(id => orderLinesIds.Contains(id));
        }
    }
}