using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class AccountEditPageViewModelTests : ViewModelTestBase<AccountEditPageViewModel>
    {
        [TestInitialize]
        public void TestInitialize()
        {
            this.accountService.SetupCurrentAccount(TestData.Accounts.CreateAccount());
        }

        [TestMethod]
        public void IsValidWhenNoDataReturnsFalse()
        {
            // Arrange
            this.viewModel.Email = null;

            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidWhenValidDataReturnsTrue()
        {
            // Arrange
            this.viewModel.Email = TestData.Accounts.Email;

            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void OnActivatePreservesFields()
        {
            // Arrange
            string email = Guid.NewGuid().ToString();
            this.viewModel.Email = email;

            // Act
            this.viewModel.Deactivate(false);
            this.viewModel.Activate();

            // Assert
            Assert.AreEqual(email, this.viewModel.Email);
        }

        [TestMethod]
        public void SaveCallsAccountServiceUpdateAndNavigatesBack()
        {
            // Arrange
            this.accountService.Setup(s => s.UpdateAccount(It.Is<Account>(a => a.Email == TestData.Accounts.Email)));

            this.navigationService.SetupGet(ns => ns.CanGoBack).Returns(true);
            this.navigationService.Setup(ns => ns.GoBack());

            // Act
            this.viewModel.Save();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }
    }
}