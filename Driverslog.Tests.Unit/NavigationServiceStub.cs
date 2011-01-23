using System;
using System.Windows.Navigation;
using Caliburn.Micro;

namespace Driverslog.Tests.Unit {
    public class NavigationServiceStub : INavigationService {
        public bool Navigate(Uri source) {
            NavigateUri = source;
            return true;
        }

        public void StopLoading() {
            throw new NotImplementedException();
        }


        public bool GoBackWasCalled;
        public void GoBack() {
            GoBackWasCalled = true;
        }

        public void GoForward() {
            throw new NotImplementedException();
        }

        public Uri Source {
            get { return NavigateUri; }
            set { NavigateUri = value; }
        }

        public bool CanGoBack {
            get { throw new NotImplementedException(); }
        }

        public bool CanGoForward {
            get { throw new NotImplementedException(); }
        }

        public Uri CurrentSource {
            get { throw new NotImplementedException(); }
        }

        public Uri NavigateUri { get; private set; }

        public event NavigatedEventHandler Navigated;
        public event NavigatingCancelEventHandler Navigating;
        
    }
}
