using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace Victoria.Test.UI {
    public class AutomationPage {

        private readonly string _page;
        private readonly PhoneApplicationFrame _applicationFrame;

        private PhoneApplicationFrame Frame {
            //get { return Application.Current.RootVisual as PhoneApplicationFrame; }
            get { return _applicationFrame; }
        }

        public PhoneApplicationPage Page {
            get { return Frame.Content as PhoneApplicationPage; }
        }

        public AutomationPage(string page, PhoneApplicationFrame applicationFrame) {
            _page = page;
            _applicationFrame = applicationFrame;
        }

        public AutomationElement<T> Find<T>(string controlName) where T : Control {
            return new AutomationElement<T>((T)Page.FindName(controlName));
        }

        public void Ready(Action action) {
            Ready((p) => action());
        }

        private NavigatedEventHandler _readyAction;

        public void Ready(Action<AutomationPage> action) {

            var handle = new AutoResetEvent(false);
            Frame.Dispatcher.BeginInvoke(() => {
                _readyAction = (s, e) => {

                    handle.Set();
                    action(this);
                    Frame.Navigated -= _readyAction;
                };
                Frame.Navigated += _readyAction;
                Frame.Navigate(new Uri(_page, UriKind.Relative));
            });
            handle.WaitOne();

        }
    }
}