using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class ProductDetailsPageViewModelTests : ViewModelTestBase<ProductDetailsPageViewModel>
    {
        private Product product;
        private Mock<IRecentProductsService> recentProductsService;
        private Mock<IProductRepository> productRepository;
        private Mock<IShoppingCart> shoppingCart;


        [TestInitialize]
        public void TestInitialize()
        {
            this.shoppingCart = new Mock<IShoppingCart>();

            this.product = TestData.Products.CreateProduct();
            this.productRepository = new Mock<IProductRepository>();
            this.productRepository.SetupGetProductById(this.product);

            this.recentProductsService = new Mock<IRecentProductsService>();

            this.viewModel.ProductRepository = this.productRepository.Object;
            this.viewModel.RecentProductsService = this.recentProductsService.Object;
            this.viewModel.ShoppingCart = this.shoppingCart.Object;

            this.viewModel.ProductId = this.product.Id;
        }

        [TestMethod]
        public void InitializeWhenProductIdIsSetLoadsProduct()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            this.productRepository.VerifyAll();
            Assert.AreEqual(this.product.Id, this.viewModel.Product.Id);
        }

        [TestMethod]
        public void InitializeShowsProgressIndicator()
        {
            // Arrange
            this.progressIndicatorService.SetupMessage();

            // Act
            this.viewModel.Activate();

            // Assert
            this.progressIndicatorService.VerifyAll();
        }

        [TestMethod]
        public void CanGoToShoppingCartWhenProductIsNullIsFalse()
        {
            // Act
            bool canAddToShoppingCart = this.viewModel.CanAddToShoppingCart;

            // Assert
            Assert.IsFalse(canAddToShoppingCart);
        }

        [TestMethod]
        public void CanGoToShoppingCartWhenProductIsNotNullIsTrue()
        {
            // Arrange
            this.viewModel.Activate();

            // Act
            bool canAddToShoppingCart = this.viewModel.CanAddToShoppingCart;

            // Assert
            Assert.IsTrue(canAddToShoppingCart);
        }

        [TestMethod]
        public void GoToShoppingCartNavigatesToShoppingCart()
        {
            // Arrange
            this.navigationService.SetupNavigateTo<ShoppingCartPageViewModel>();

            // Act
            this.viewModel.GoToShoppingCart();

            // Arrange
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void AddToShoppingCartAddsToShoppingCartAndShowsMessage()
        {
            // Arrange
            this.shoppingCart.SetupAddProduct(this.product);
            this.messageService.SetupCustomMessageBoxResult(CustomMessageBoxResult.LeftButton);
            this.navigationService.SetupNavigateTo<ShoppingCartPageViewModel>();

            this.viewModel.Activate();

            // Act
            this.viewModel.AddToShoppingCart();

            // Assert
            this.shoppingCart.VerifyAll();
            this.messageService.VerifyAll();
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void IsPopupVisibleIsFalseByDefault()
        {
            // Assert
            Assert.IsFalse(this.viewModel.IsPopupVisible);
        }

        [TestMethod]
        public void ShowImagePopupShowsPopup()
        {
            // Act
            Uri imageSource = this.product.Images[0];
            this.viewModel.ShowImagePopup(imageSource);

            // Assert
            Assert.IsTrue(this.viewModel.IsPopupVisible);
            Assert.AreEqual(imageSource, this.viewModel.PopupImageSource);
        }

        [TestMethod]
        public void ActivateMarksProductInRecentProductService()
        {
            // Arrange
            this.recentProductsService.SetupAddProduct(this.product);

            // Act
            this.viewModel.Activate();

            // Assert
            this.recentProductsService.VerifyAll();
        }
    }
}