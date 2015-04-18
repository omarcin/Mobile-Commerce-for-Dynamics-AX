using Caliburn.Micro;
using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace mAxCommerce.WindowsPhone.Ninject
{
    public sealed class NinjectPhoneContainer : IPhoneContainer, IDisposable
    {
        private readonly IKernel kernel;

        public event Action<object> Activated = delegate { };

        public NinjectPhoneContainer(Frame rootFrame, params INinjectModule[] ninjectModules)
        {
            var modules = new List<INinjectModule>(ninjectModules);
            modules.Add(new PhoneServicesModule(rootFrame, this));
            this.kernel = new StandardKernel(modules.ToArray());
        }

        public void RegisterWithPhoneService(Type service, string phoneStateKey, Type implementation)
        {
            throw new NotImplementedException();
        }

        public void RegisterWithAppSettings(Type service, string appSettingsKey, Type implementation)
        {
            throw new NotImplementedException();
        }

        public T GetInstance<T>()
        {
            return (T)this.GetInstance(typeof(T));
        }

        public object GetInstance(Type service)
        {
            var instance = this.kernel.Get(service);
            this.Activated(instance);
            return instance;
        }

        public IEnumerable<T> GetAllInstances<T>()
        {
            return (IEnumerable<T>)this.GetAllInstances(typeof(T));
        }

        public IEnumerable<object> GetAllInstances(Type service)
        {
            var instances = this.kernel.GetAll(service);

            foreach (var instance in instances)
            {
                this.Activated(instance);
            }

            return instances;
        }

        private class PhoneServicesModule : NinjectModule
        {
            private readonly Frame rootFrame;
            private readonly IPhoneContainer phoneContainer;

            public PhoneServicesModule(Frame rootFrame, IPhoneContainer phoneContainer)
            {
                this.phoneContainer = phoneContainer;
                this.rootFrame = rootFrame;
            }

            public override void Load()
            {
                var phoneService = new PhoneApplicationServiceAdapter(this.rootFrame);
                var navigationService = new FrameAdapter(this.rootFrame, false);

                this.Bind<Frame>().ToConstant(this.rootFrame);
                this.Bind<IPhoneContainer>().ToConstant(this.phoneContainer);
                this.Bind<INavigationService>().ToConstant(navigationService);
                this.Bind<IPhoneService>().ToConstant(phoneService);
                this.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();
                this.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
                this.Bind<IVibrateController>().To<SystemVibrateController>().InSingletonScope();
                this.Bind<ISoundEffectPlayer>().To<XnaSoundEffectPlayer>().InSingletonScope();
                this.Bind<StorageCoordinator>().To<StorageCoordinator>().InSingletonScope();
                this.Bind<TaskController>().To<TaskController>().InSingletonScope();

                this.Bind<IStorageMechanism>().To<PhoneStateStorageMechanism>().InSingletonScope();
                this.Bind<IStorageMechanism>().To<AppSettingsStorageMechanism>().InSingletonScope();
            }
        }

        public void Dispose()
        {
            if (this.kernel != null)
            {
                this.kernel.Dispose();
            }
        }
    }
}
