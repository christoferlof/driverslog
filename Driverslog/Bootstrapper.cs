using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Driverslog.ViewModels;

namespace Driverslog {
    public class Bootstrapper : PhoneBootstrapper {
        
        private PhoneContainer _container;
        
        protected override void Configure() {
            _container = new PhoneContainer();

            //register viewmodels
            _container.RegisterSingleton(typeof(MainPageViewModel),"MainPageViewModel",typeof(MainPageViewModel));
            _container.RegisterSingleton(typeof(CreateViewModel), "CreateViewModel", typeof(CreateViewModel));

            //register services
            _container.RegisterInstance(typeof(INavigationService), null, new FrameAdapter(RootFrame));
            _container.RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(PhoneService));
        }

        protected override object GetInstance(Type service, string key) {
            return _container.GetInstance(service,key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            _container.BuildUp(instance);
        }
    }
}