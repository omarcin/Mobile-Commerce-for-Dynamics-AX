using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.WindowsPhone.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace mAxCommerce.WindowsPhone.ViewModels
{
    public sealed class ConductorViewModelBase 
    {
        public class AllActive : Conductor<IViewModel>.Collection.AllActive, IViewModel
        {
            [Inject]
            public INavigationService NavigationService { get; set; }

            [Inject]
            public IAccountService AccountService { get; set; }

            [Inject]
            public IMessageService MessageService { get; set; }

            [Inject]
            public IProgressIndicatorService ProgressIndicatorService { get; set; }

            public AllActive()
                : base(true)
            {
            }
        }

        public class OneActive<TItem> : Conductor<TItem>.Collection.OneActive, IViewModel where TItem : class, IViewModel
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
}
