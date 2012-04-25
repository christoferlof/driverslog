using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Driverslog.Services;
using Driverslog.ViewModels;
using Microsoft.Phone.Tasks;

namespace Driverslog {
    public class Bootstrapper : PhoneBootstrapper {

        private PhoneContainer _container;

        protected override void Configure() {
            _container = new PhoneContainer();

            //register viewmodels
            _container.RegisterSingleton(typeof(MainPageViewModel), "MainPageViewModel", typeof(MainPageViewModel));
            _container.RegisterPerRequest(typeof(CreateViewModel), "CreateViewModel", typeof(CreateViewModel));
            _container.RegisterPerRequest(typeof(EditViewModel), "EditViewModel", typeof(EditViewModel));
            _container.RegisterPerRequest(typeof(CreateExpenseViewModel), "CreateExpenseViewModel", typeof(CreateExpenseViewModel));
            _container.RegisterPerRequest(typeof(EditExpenseViewModel), "EditExpenseViewModel", typeof(EditExpenseViewModel));
            _container.RegisterPerRequest(typeof(SettingsViewModel), "SettingsViewModel", typeof(SettingsViewModel));

            //register services
            _container.RegisterInstance(typeof(INavigationService), null, new FrameAdapter(RootFrame));
            _container.RegisterInstance(typeof(IPhoneService), null, new PhoneApplicationServiceAdapter(PhoneService));
            _container.RegisterInstance(typeof(IMessageBoxService), null, new MessageBoxService());
            _container.RegisterInstance(typeof(IAnalyticsService),null, new AnalyticsService());
            _container.RegisterInstance(typeof(ITrialService), null, new TrialService(new AnalyticsService()));

        }

        protected override object GetInstance(Type service, string key) {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service) {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance) {
            _container.BuildUp(instance);
        }

        private const string FlurryKey = "KJU8ELY9LC2WN4UA83PU";

        protected override void OnLaunch(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e) {
            base.OnLaunch(sender, e);
            FlurryWP7SDK.Api.StartSession(FlurryKey);
        }

        protected override void OnActivate(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e) {
            base.OnActivate(sender, e);
            FlurryWP7SDK.Api.StartSession(FlurryKey);
        }
    }
}