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
    public class MainPageViewModelTests : ViewModelTestBase<MainPageViewModel>
    {
        private Mock<IRecentProductsViewModel> recentProductsViewModel;
        private Mock<IProductsByCategoryViewModel> productsByCategoryViewModel;        
        
        [TestInitialize]
        public void TestInitialize()
        {
            this.recentProductsViewModel = new Mock<IRecentProductsViewModel>();
            this.productsByCategoryViewModel = new Mock<IProductsByCategoryViewModel>();

            this.viewModel.RecentProducts = this.recentProductsViewModel.Object;
            this.viewModel.ProductsByCategory = this.productsByCategoryViewModel.Object;
        }

        [TestMethod]
        public void ActivateActivatesChildViewModels()
        {
            // Arrange
            this.recentProductsViewModel.Setup(vm => vm.Activate());
            this.productsByCategoryViewModel.Setup(vm => vm.Activate());

            // Act
            this.viewModel.Activate();

            // Assert
            this.recentProductsViewModel.VerifyAll();
            this.productsByCategoryViewModel.VerifyAll();
        }

        [TestMethod]
        public void DeactivateDeactivatesChildViewModels()
        {
            // Arrange
            this.recentProductsViewModel.Setup(vm => vm.Activate());
            this.productsByCategoryViewModel.Setup(vm => vm.Activate());
            this.viewModel.Activate();

            this.recentProductsViewModel.Setup(vm => vm.Deactivate(false));
            this.productsByCategoryViewModel.Setup(vm => vm.Deactivate(false));

            // Act
            this.viewModel.Deactivate(false);

            // Assert
            this.recentProductsViewModel.VerifyAll();
            this.productsByCategoryViewModel.VerifyAll();
        }

        [TestMethod]
        public void NavigateToAccountWhenLoggedInNavigatesToAccountDetailsPage()
        {
            // Arrange
            this.accountService.Setup(s => s.IsLoggedIn()).Returns(true);

            this.navigationService.SetupNavigateTo<AccountDetailsPageViewModel>();

            // Act
            this.viewModel.NavigateToAccount();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void NavigateToAccountWhenNotLoggedInNavigatesToLoginPage()
        {
            // Arrange
            this.accountService.Setup(s => s.IsLoggedIn()).Returns(false);

            string accountDetailsUri = UriHelper.GetUriFor<AccountDetailsPageViewModel>();
            this.navigationService.SetupNavigateToWithParam<LoginPageViewModel>(vm => vm.NextPageUriString, accountDetailsUri);

            // Act
            this.viewModel.NavigateToAccount();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }
    }
}