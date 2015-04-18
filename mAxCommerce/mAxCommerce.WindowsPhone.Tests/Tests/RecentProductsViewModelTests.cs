using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.Tests.Tests
{
    [TestClass]
    public class RecentProductsViewModelTests : ViewModelTestBase<RecentProductsViewModel>
    {
        private List<Product> recentProducts;
        private Mock<IRecentProductsService> recentProductsService;
        
        [TestInitialize]
        public void TestInitialize()
        {
            this.recentProducts = TestData.Products.CreateProducts();
            this.recentProductsService = new Mock<IRecentProductsService>();
            this.recentProductsService.SetupGetProducts(this.recentProducts);

            this.viewModel.RecentProductsService = this.recentProductsService.Object;
        }

        [TestMethod]
        public void RecentProductsAreLoadedFromRecentProductsService()
        {
            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                this.recentProducts.Select(p => p.Id).ToList(),
                this.viewModel.RecentProducts.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void ProgressIndicatorIsShownWhileLoadingRecentProducts()
        {
            // Arrange
            this.progressIndicatorService.SetupMessage();

            // Act
            this.viewModel.Activate();

            // Assert
            this.progressIndicatorService.VerifyAll();
        }
        
        [TestMethod]
        public void RecentProductsAreReloadedOnActivate()
        {
            var newRecentProducts = TestData.Products.CreateProducts();
            this.viewModel.Activate();
            this.viewModel.Deactivate(false);
            this.recentProductsService.SetupGetProducts(newRecentProducts);

            // Act
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                newRecentProducts.Select(p => p.Id).ToList(),
                this.viewModel.RecentProducts.Select(p => p.Id).ToList());
        }

        [TestMethod]
        public void NavigateToProductDetailsPassesProductToProductDetailsPage()
        {
            // Arrange
            var product = this.recentProducts[1];

            this.navigationService.SetupNavigateToWithParam<ProductDetailsPageViewModel>(vm => vm.ProductId, product.Id);

            this.viewModel.Activate();

            // Act
            this.viewModel.NavigateToProductDetails(product);

            // Assert
            this.navigationService.VerifyAll();
        }
    }
}
