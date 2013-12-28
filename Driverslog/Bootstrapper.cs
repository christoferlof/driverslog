using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Driverslog.Services;
using Driverslog.ViewModels;
using Microsoft.ApplicationInsights.Telemetry.WindowsStore;
using Microsoft.Phone.Tasks;

namespace Driverslog {
    using System.Windows.Navigation;

    using Microsoft.Phone.Controls;

    public class Bootstrapper : PhoneBootstrapper {

        private PhoneContainer _container;

        private PhoneApplicationFrame _rootFrame;

        private bool _reset;

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

            //intercept navigation for app resume
            _rootFrame.Navigated += RootFrameOnNavigated;
            _rootFrame.Navigating += RootFrameOnNavigating;

        }

        private void RootFrameOnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (_reset && e.IsCancelable && e.Uri.OriginalString == "/MainPage.xaml") {
                e.Cancel = true;
                _reset = false;
            }
        }

        private void RootFrameOnNavigated(object sender, NavigationEventArgs e)
        {
            _reset = e.NavigationMode == NavigationMode.Reset;
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

        protected override void OnUnhandledException(object sender, System.Windows.ApplicationUnhandledExceptionEventArgs e) {
            ClientAnalyticsChannel.Default.LogEvent("exception", new Dictionary<string, object> { {"message", e.ExceptionObject.Message}});
            base.OnUnhandledException(sender, e);
        }

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            _rootFrame = new PhoneApplicationFrame();
            return _rootFrame;
        }
    }
}