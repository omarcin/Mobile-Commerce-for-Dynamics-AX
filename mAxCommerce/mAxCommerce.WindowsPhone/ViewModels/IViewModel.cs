using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public interface IViewModel : IScreen
    {
        [Inject]
        INavigationService NavigationService { get; set; }

        [Inject]
        IAccountService AccountService { get; set; }

        [Inject]
        IMessageService MessageService { get; set; }

        [Inject]
        IProgressIndicatorService ProgressIndicatorService { get; set; }
    }
}
