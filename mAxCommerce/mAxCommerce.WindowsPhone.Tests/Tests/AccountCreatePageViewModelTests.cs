using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class AccountCreatePageViewModelTests : ViewModelTestBase<AccountCreatePageViewModel>
    {
        [TestMethod]
        public void IsValidWhenNoDataReturnsFalse()
        {
            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidWhenValidDataReturnsTrue()
        {
            // Arrange
            this.FillWithCorrectData();

            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void IsValidWhenDoesNotAcceptTermsReturnsFalse()
        {
            // Arrange
            this.viewModel.AcceptsTerms = false;
            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;
            this.viewModel.PasswordRepeat = TestData.Accounts.Password;

            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidWhenPasswordsDoNotMatchReturnsFalse()
        {
            // Arrange
            this.viewModel.AcceptsTerms = true;
            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;
            this.viewModel.PasswordRepeat = TestData.Accounts.Password2;

            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void RegisterWhenNoNextPageUriNavigatesToMainPage()
        {
            // Arrange
            string uriString = UriHelper.GetUriFor<MainPageViewModel>();

            this.accountService.Setup(service => service.Register(It.IsAny<Account>())).Returns(true);
            this.navigationService.Setup(service => service.Navigate(It.Is<Uri>(u => u.OriginalString == uriString)));

            this.FillWithCorrectData();

            // Act
            this.viewModel.Register();

            // Assert
            this.navigationService.VerifyAll();
            this.accountService.VerifyAll();
        }
  
        [TestMethod]
        public void RegisterNavigatesToSpecifiedNextPage()
        {
            // Arrange
            const string NextPageUriString = "/Views/NextPage.xaml";

            this.accountService.Setup(service => service.Register(It.IsAny<Account>())).Returns(true);
            this.navigationService.Setup(service => service.Navigate(It.Is<Uri>(u => u.OriginalString == NextPageUriString)));

            this.viewModel.NextPageUriString = NextPageUriString;

            this.FillWithCorrectData();

            // Act
            this.viewModel.Register();

            // Assert
            this.navigationService.VerifyAll();
            this.accountService.VerifyAll();
        }

        [TestMethod]
        public void RegisterWhenFailedShowsMessage()
        {
            // Arrange
            const string NextPageUriString = "/Views/NextPage.xaml";

            this.accountService.Setup(service => service.Register(It.IsAny<Account>())).Returns(false);
            this.messageService.Setup(service => service.ShowMessage(It.IsAny<string>(), It.IsAny<string>()));

            this.viewModel.NextPageUriString = NextPageUriString;

            this.FillWithCorrectData();

            // Act
            this.viewModel.Register();

            // Assert
            this.messageService.VerifyAll();
            this.accountService.VerifyAll();
        }

        [TestMethod]
        public void OnActivatePreservesFields()
        {
            // Arrange
            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;
            this.viewModel.PasswordRepeat = TestData.Accounts.Password2;

            // Act
            this.viewModel.Deactivate(false);
            this.viewModel.Activate();

            // Assert
            Assert.AreEqual(TestData.Accounts.Email, this.viewModel.Email);
            Assert.AreEqual(TestData.Accounts.Password, this.viewModel.Password);
            Assert.AreEqual(TestData.Accounts.Password2, this.viewModel.PasswordRepeat);
        }
        
        private void FillWithCorrectData()
        {
            this.viewModel.AcceptsTerms = true;
            this.viewModel.Email = TestData.Accounts.Email;
            this.viewModel.Password = TestData.Accounts.Password;
            this.viewModel.PasswordRepeat = TestData.Accounts.Password;
        }
    }
}