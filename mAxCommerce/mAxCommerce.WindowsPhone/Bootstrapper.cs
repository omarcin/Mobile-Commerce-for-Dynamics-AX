using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using mAxCommerce.WindowsPhone.Ninject;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace mAxCommerce.WindowsPhone
{
    public class Bootstrapper : PhoneBootstrapper
    {
        NinjectPhoneContainer container;

        protected override void Configure()
        {
            LogManager.GetLog = _ => new DebugLog();

            this.container = new NinjectPhoneContainer(
                this.RootFrame, 
                new WindowsPhoneNinjectModule(),
                new DemoServicesModule(),
                new StorageHandlersModule(),
                new ViewModelsModule());

            StorageCoordinator coordinator = this.container.GetInstance<StorageCoordinator>();
            coordinator.Start();

            TaskController taskController = this.container.GetInstance<TaskController>();
            taskController.Start();

            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            // App Bar Conventions
            ConventionManager.AddElementConvention<BindableAppBarButton>(
                Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
                Control.IsEnabledProperty, "DataContext", "Click");  

            // Telerik
            ConventionManager.AddElementConvention<RadPasswordBox>(
                RadPasswordBox.PasswordProperty, "Password", "PasswordChanged");
        }

        protected override object GetInstance(Type service, string key)
        {
            return this.container.GetInstance(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return this.container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            throw new NotImplementedException();
        }

        protected override void OnLaunch(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
            base.OnLaunch(sender, e);
            new NavigationSetup().SetupNavigation(this.RootFrame);
        }

        private class DebugLog : ILog
        {
            public void Error(Exception exception)
            {
                Debug.WriteLine(exception);
            }

            public void Info(string format, params object[] args)
            {
                Debug.WriteLine(string.Format(format, args));
            }

            public void Warn(string format, params object[] args)
            {
                Debug.WriteLine(string.Format(format, args));
            }
        }
    }
}
