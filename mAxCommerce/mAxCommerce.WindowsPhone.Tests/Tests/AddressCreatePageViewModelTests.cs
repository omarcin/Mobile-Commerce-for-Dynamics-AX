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
    public class AddressCreatePageViewModelTests : ViewModelTestBase<AddressCreatePageViewModel>
    {
        private Account account;

        [TestInitialize]
        public void TestInitialize()
        {
            this.account = TestData.Accounts.CreateAccount();
            this.accountService.SetupCurrentAccount(this.account);
        }

        [TestMethod]
        public void IsValidWhenDataIsMissingReturnsFalse()
        {
            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidWhenDataIsCorrectReturnsTrue()
        {
            // Arrange
            this.FillWithCorrectData();

            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.IsTrue(isValid);
        }
  
        [TestMethod]
        public void FirstAndLastNameAreDefaulted()
        {
            // Arrange
            string firstName = this.account.DeliveryAddresses.Last().FirstName;
            string lastName = this.account.DeliveryAddresses.Last().LastName;
            
            // Act
            this.viewModel.Activate();
         
            // Assert
            Assert.AreEqual(firstName, this.viewModel.FirstName);
            Assert.AreEqual(lastName, this.viewModel.LastName);
        }
  
        [TestMethod]
        public void FirstAndLastNameWhenNoPreviousAddressesAreNotDefaulted()
        {
            // Arrange
            this.accountService.SetupCurrentAccount(TestData.Accounts.CreateAccount(0));
            
            // Act
            this.viewModel.Activate();

            // Assert
            Assert.IsNull(this.viewModel.FirstName);
            Assert.IsNull(this.viewModel.LastName);
        }
  
        [TestMethod]
        public void CreateAddressUpdatesAccountAndNavigatesBack()
        {
            // Arrange
            this.accountService.Setup(s => s.UpdateAccount(It.Is<Account>(
                a => a.Email == this.account.Email &&
                     a.DeliveryAddresses.Count == this.account.DeliveryAddresses.Count + 1 &&
                     a.DeliveryAddresses.Last().Street == TestData.Addresses.Street)));
            
            this.navigationService.SetupGet(ns => ns.CanGoBack).Returns(true);
            this.navigationService.Setup(ns => ns.GoBack());

            this.FillWithCorrectData();

            // Act
            this.viewModel.CreateAddress();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }
  
        private void FillWithCorrectData()
        {
            // Arrange
            this.viewModel.FirstName = TestData.Addresses.FirstName;
            this.viewModel.LastName = TestData.Addresses.LastName;
            this.viewModel.Street = TestData.Addresses.Street;
            this.viewModel.Number = TestData.Addresses.Number;
            this.viewModel.PostalCode = TestData.Addresses.PostalCode;
            this.viewModel.City = TestData.Addresses.City;
            this.viewModel.Country = TestData.Addresses.Country;
        }
    }
}