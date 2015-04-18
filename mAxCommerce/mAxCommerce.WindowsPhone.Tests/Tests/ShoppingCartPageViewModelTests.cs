using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class ShoppingCartPageViewModelTests : ViewModelTestBase<ShoppingCartPageViewModel>
    {
        private Mock<IShoppingCart> shoppingCart;

        private List<Product> products;

        [TestInitialize]
        public void TestInitialize()
        {
            this.products = TestData.Products.CreateProducts();
            this.shoppingCart = new Mock<IShoppingCart>();
            this.shoppingCart.SetupGetProducts(this.products);
            this.viewModel.ShoppingCart = this.shoppingCart.Object;
        }

        [TestMethod]
        public void ProductsEqualShoppingCartProduct()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                this.products.Select(p => p.Id).ToList(),
                this.viewModel.Products.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void TotalAmountEqualsSumOfProductPrices()
        {
            // Arrange
            decimal sum = this.products.Sum(p => p.Price);
            this.viewModel.Activate();

            // Act
            decimal totalAmount = this.viewModel.TotalAmount;

            // Assert
            Assert.AreEqual(sum, totalAmount);
        }

        [TestMethod]
        public void CanCheckoutWhenNoProductsIsFalse()
        {
            // Arrange
            this.shoppingCart.SetupGetProducts(new List<Product>());
            this.viewModel.Activate();

            //Act
            bool canCheckout = this.viewModel.CanCheckout;

            //Assert
            Assert.IsFalse(canCheckout);
        }

        [TestMethod]
        public void CanCheckoutWhenProductsExistIsTrue()
        {
            // Arrange
            this.viewModel.Activate();

            // Act
            bool canCheckout = this.viewModel.CanCheckout;

            // Assert
            Assert.IsTrue(canCheckout);
        }

        [TestMethod]
        public void CheckoutWhenLoggedInNavigatesToAddressSelectPage()
        {
            // Arrange
            this.accountService.Setup(s => s.IsLoggedIn()).Returns(true);

            this.navigationService.SetupNavigateTo<AddressSelectPageViewModel>();

            // Act
            this.viewModel.Checkout();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void CheckoutWhenNotLoggedInNavigatesToLoginPage()
        {
            // Arrange
            this.accountService.Setup(s => s.IsLoggedIn()).Returns(false);
            
            string uriSelect = UriHelper.GetUriFor<AddressSelectPageViewModel>();
            this.navigationService.SetupNavigateToWithParam<LoginPageViewModel>(vm => vm.NextPageUriString, uriSelect);

            // Act
            this.viewModel.Checkout();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }
    }
}