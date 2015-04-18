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
    public class AccountDetailsPageViewModelTests : ViewModelTestBase<AccountDetailsPageViewModel>
    {
        private Account account;

        [TestInitialize]
        public void TestInitialize()
        {
            this.account = TestData.Accounts.CreateAccount();
        }

        [TestMethod]
        public void IsValidDelegatesToAccountValid()
        {
            // Arrange
            var accountMock = new Mock<Account>();
            accountMock.Setup(a => a.Validate()).Returns(false);

            this.accountService.SetupGet(s => s.CurrentAccount).Returns(accountMock.Object);

            // Act
            bool isValid = this.viewModel.IsValid;

            // Assert
            Assert.AreEqual(false, isValid);
            this.accountService.VerifyAll();
            accountMock.VerifyAll();
        }

        [TestMethod]
        public void CreateAddressNavigatesToAddressCreatePage()
        {
            // Arrange
            this.navigationService.SetupNavigateTo<AddressCreatePageViewModel>();

            // Act
            this.viewModel.CreateAddress();

            // Assert
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void LogOutNavigatesBack()
        {
            // Arrange
            this.accountService.Setup(s => s.LogOut());

            this.navigationService.SetupGet(s => s.CanGoBack).Returns(true);
            this.navigationService.Setup(s => s.GoBack());

            // Act
            this.viewModel.LogOut();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void LogOutWhenCanNotGoBackNavigatesToMainPage()
        {
            // Arrange
            this.accountService.Setup(s => s.LogOut());

            this.navigationService.SetupNavigateTo<MainPageViewModel>();
            this.navigationService.SetupGet(s => s.CanGoBack).Returns(false);

            // Act
            this.viewModel.LogOut();

            // Assert
            this.accountService.VerifyAll();
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void EditNavigatesToAccountEditPage()
        {
            // Arrange
            this.navigationService.SetupNavigateTo<AccountEditPageViewModel>();

            // Act
            this.viewModel.Edit();

            // Assert
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void EditAddressNavigatesToAddressEditPage()
        {
            // Arrange
            Address address = this.account.DeliveryAddresses[0];
            this.navigationService.SetupNavigateToWithParam<AddressEditPageViewModel>(vm => vm.AddressId, address.Id);

            this.accountService.SetupCurrentAccount(this.account);

            // Act
            this.viewModel.EditAddress(address);

            // Assert
            this.navigationService.VerifyAll();
        }

        [TestMethod]
        public void OnActivateRefreshesEmail()
        {
            // Arrange
            this.accountService.SetupCurrentAccount(this.account);

            this.viewModel.Activate();
            Assert.AreEqual(this.account.Email, this.viewModel.Email);

            // Act
            this.account.Email = Guid.NewGuid().ToString();
            this.viewModel.Deactivate(false);
            this.viewModel.Activate();

            // Assert
            Assert.AreEqual(this.account.Email, this.viewModel.Email);
        }

        [TestMethod]
        public void OnActivateRefreshesDeliveryAddresses()
        {
            // Arrange
            this.accountService.SetupCurrentAccount(this.account);

            this.viewModel.Activate();
            CollectionAssert.AreEqual(
                this.account.DeliveryAddresses.Select(a => a.Id).ToList(),
                this.viewModel.DeliveryAddresses.Select(a => a.Id).ToList());

            // Act
            this.account.DeliveryAddresses.RemoveAt(0);
            this.account.DeliveryAddresses.Add(TestData.Addresses.CreateAddress());

            this.viewModel.Deactivate(false);
            this.viewModel.Activate();

            // Assert
            CollectionAssert.AreEqual(
                this.account.DeliveryAddresses.Select(a => a.Id).ToList(),
                this.viewModel.DeliveryAddresses.Select(a => a.Id).ToList());
        }
    }
}