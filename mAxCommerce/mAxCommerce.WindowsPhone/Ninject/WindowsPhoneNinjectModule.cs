using Caliburn.Micro;
using mAxCommerce.Core;
using mAxCommerce.Core.Demo;
using mAxCommerce.WindowsPhone.Services;
using mAxCommerce.WindowsPhone.ViewModels;
using mAxCommerce.WindowsPhone.ViewModelsStorage;
using Ninject.Modules;
using System;
using System.Linq;

namespace mAxCommerce.WindowsPhone.Ninject
{
    public class WindowsPhoneNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IMessageService>().To<MessageService>();
            this.Bind<IProgressIndicatorService>().To<ProgressIndicatorService>().InSingletonScope();
            this.Bind<IObjectStorage>().To<WindowPhoneObjectStorage>();
        }
    }
}
