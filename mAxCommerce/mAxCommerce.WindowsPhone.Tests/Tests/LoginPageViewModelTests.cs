using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class LoginPageViewModelTests : ViewModelTestBase<LoginPageViewModel>
    {
        [TestMethod]
        public void CanLogInWhenNoDataReturnsFalse()
        {
            // Act
            bool canLogIn = this.viewModel.CanLogIn;

            // Assert
            Assert.IsFalse(canLogIn);
        }

        [TestMethod]
        public void CanLogInWhenAllDataReturnsTrue()
        {
            // Arrange
            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;

            // Act
            bool canLogIn = this.viewModel.CanLogIn;

            // Assert
            Assert.IsTrue(canLogIn);
        }

        [TestMethod]
        public void LogInOnSuccessNavigatesToNextPageUriString()
        {
            // Arrange
            const string UriString = "/Views/Uri.xaml";

            this.accountService
                .Setup(s => s.LogIn(TestData.Accounts.Email, TestData.Accounts.Password))
                .Returns(true);

            this.navigationService.SetupNavigateToUri(UriString);

            this.viewModel.NextPageUriString = UriString;

            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;

            // Act
            this.viewModel.LogIn();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void LogInOnSuccessWhenNoNextPageNavigatesToMainPage()
        {
            // Arrange
            this.accountService
                .Setup(s => s.LogIn(TestData.Accounts.Email, TestData.Accounts.Password))
                .Returns(true);

            this.navigationService.SetupNavigateTo<MainPageViewModel>();

            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;

            // Act
            this.viewModel.LogIn();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void LogInOnFailureShowsMessage()
        {
            // Arrange
            this.accountService
                .Setup(s => s.LogIn(TestData.Accounts.Email, TestData.Accounts.Password))
                .Returns(false);

            this.messageService.Setup(s => s.ShowMessage(It.IsAny<string>(), It.IsAny<string>()));

            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;

            // Act
            this.viewModel.LogIn();

            // Assert
            this.accountService.VerifyAll();
            this.messageService.VerifyAll();
        }

        [TestMethod]
        public void CreateAccountNavigatesToAccountCreatePageViewModel()
        {
            // Arrange
            const string NextPageUriString = "/Views/NextPage.xaml";
            this.navigationService.SetupNavigateToWithParam<AccountCreatePageViewModel>(vm => vm.NextPageUriString, NextPageUriString);
            this.viewModel.NextPageUriString = NextPageUriString;

            // Act
            this.viewModel.CreateAccount();

            // Assert
            this.navigationService.VerifyAll();
        }
    }
}