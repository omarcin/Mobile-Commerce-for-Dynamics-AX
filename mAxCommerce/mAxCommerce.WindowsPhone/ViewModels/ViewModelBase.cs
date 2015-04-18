using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using Ninject;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public class ViewModelBase : Screen, IViewModel
    {
        [Inject]
        public INavigationService NavigationService { get; set; }

        [Inject]
        public IAccountService AccountService { get; set; }

        [Inject]
        public IMessageService MessageService { get; set; }

        [Inject]
        public IProgressIndicatorService ProgressIndicatorService { get; set; }
    }
}
