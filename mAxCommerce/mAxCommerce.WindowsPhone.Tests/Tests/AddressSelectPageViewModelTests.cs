using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Tests.Helpers;
using mAxCommerce.WindowsPhone.ViewModels;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public class AddressSelectPageViewModelTests : ViewModelTestBase<AddressSelectPageViewModel>
    {
        private Account account;
        private Address address;

        [TestInitialize]
        public void TestInitialize()
        {
            this.account = TestData.Accounts.CreateAccount();
            this.accountService.SetupCurrentAccount(this.account);

            this.address = this.account.DeliveryAddresses[3];

            this.viewModel.Activate();
        }

        [TestMethod]
        public void DeliveryAddressesAreEqualToAccountDeliveryAddresses()
        {
            // Assert
            CollectionAssert.AreEqual(
                this.account.DeliveryAddresses.Select(a => a.Id).ToList(),
                this.viewModel.DeliveryAddresses.Select(a => a.Id).ToList());
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
        public void SelectAddressPassesAddressIdToOrderConfirmPage()
        {
            // Arrange
            this.navigationService.SetupNavigateToWithParam<OrderConfirmPageViewModel>(vm => vm.DeliveryAddressId, this.address.Id);

            // Act
            this.viewModel.SelectAddress(this.address);

            // Assert
            this.navigationService.VerifyAll();
        }
    }
}