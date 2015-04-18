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
    public class AddressEditPageViewModelTests : ViewModelTestBase<AddressEditPageViewModel>
    {
        private Account account;
        private Address address;
        private int addressIndex;

        [TestInitialize]
        public void TestInitialize()
        {
            this.account = TestData.Accounts.CreateAccount();
            this.accountService.SetupCurrentAccount(this.account);

            this.addressIndex = 3;
            this.address = this.account.DeliveryAddresses[this.addressIndex];
            this.viewModel.AddressId = this.address.Id;
        }

        [TestMethod]
        public void ViewModelLoadsDataFromAddressId()
        {
            // Assert
            Assert.AreEqual(this.address.City, this.viewModel.City);
            Assert.AreEqual(this.address.Country, this.viewModel.Country);
            Assert.AreEqual(this.address.FirstName, this.viewModel.FirstName);
            Assert.AreEqual(this.address.Id, this.viewModel.AddressId);
            Assert.AreEqual(this.address.LastName, this.viewModel.LastName);
            Assert.AreEqual(this.address.Number, this.viewModel.Number);
            Assert.AreEqual(this.address.PostalCode, this.viewModel.PostalCode);
            Assert.AreEqual(this.address.Street, this.viewModel.Street);
        }

        [TestMethod]
        public void SaveUpdatesAccountAndNavigatesBack()
        {
            // Arrange
            Address addressUpdate = TestData.Addresses.CreateAddress();

            this.accountService.Setup(s => s.UpdateAccount(It.Is<Account>(
                a => a.Email == this.account.Email &&
                     a.DeliveryAddresses.Count == this.account.DeliveryAddresses.Count &&
                     a.DeliveryAddresses[this.addressIndex].Id == this.address.Id &&
                     a.DeliveryAddresses[this.addressIndex].Street == addressUpdate.Street)));

            this.navigationService.SetupGet(ns => ns.CanGoBack).Returns(true);
            this.navigationService.Setup(ns => ns.GoBack());

            this.viewModel.City = addressUpdate.City; 
            this.viewModel.Country = addressUpdate.Country;    
            this.viewModel.FirstName = addressUpdate.FirstName; 
            this.viewModel.LastName = addressUpdate.LastName; 
            this.viewModel.Number = addressUpdate.Number; 
            this.viewModel.PostalCode = addressUpdate.PostalCode; 
            this.viewModel.Street = addressUpdate.Street;

            // Act
            this.viewModel.Save();

            // Assert
            this.navigationService.VerifyAll();
            this.accountService.VerifyAll();
        }

        [TestMethod]
        public void DeleteUpdatesAccountAndNavigatesBack()
        {
            // Arrange
            this.accountService.Setup(s => s.UpdateAccount(It.Is<Account>(
                a => a.Email == this.account.Email &&
                     a.DeliveryAddresses.Count == this.account.DeliveryAddresses.Count - 1 &&
                     a.DeliveryAddresses.All(_ad => _ad.Id != this.address.Id))));

            this.navigationService.SetupGet(ns => ns.CanGoBack).Returns(true);
            this.navigationService.Setup(ns => ns.GoBack());

            // Act
            this.viewModel.Delete();

            // Assert
            this.navigationService.VerifyAll();
            this.accountService.VerifyAll();
        }
    }
}