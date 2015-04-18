using System;
using System.Linq;
using Caliburn.Micro;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using Moq;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using mAxCommerce.WindowsPhone.ViewModels;

namespace mAxCommerce.WindowsPhone.Tests
{
    [TestClass]
    public abstract class ViewModelTestBase<TViewModel> where TViewModel : IViewModel, new()
    {
        protected Mock<IAccountService> accountService;

        protected Mock<INavigationService> navigationService;

        protected Mock<IMessageService> messageService;

        protected Mock<IProgressIndicatorService> progressIndicatorService;

        protected TViewModel viewModel;

        [TestInitialize]
        public void TestInitializeBase()
        {
            this.accountService = new Mock<IAccountService>();
            this.navigationService = new Mock<INavigationService>();
            this.messageService = new Mock<IMessageService>();
            this.progressIndicatorService = new Mock<IProgressIndicatorService>();

            this.viewModel = new TViewModel();

            this.viewModel.AccountService = this.accountService.Object;
            this.viewModel.NavigationService = this.navigationService.Object;
            this.viewModel.MessageService = this.messageService.Object;
            this.viewModel.ProgressIndicatorService = this.progressIndicatorService.Object;
        }
    }
}
