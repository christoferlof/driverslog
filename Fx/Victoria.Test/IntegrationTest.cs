using System.Threading;
using Microsoft.Phone.Controls;
using Victoria.Test.UI;

namespace Victoria.Test {
    public abstract class IntegrationTest {
        public PhoneApplicationFrame RootFrame { get; set; }

        protected AutomationPage Page(string viewUri) {
            return new AutomationPage(viewUri,RootFrame);
        }
    }
}