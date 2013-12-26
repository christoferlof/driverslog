using System.Windows;
using System.Windows.Navigation;
using Microsoft.ApplicationInsights.Telemetry.WindowsStore;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;


namespace Driverslog {
    public partial class App : Application {
        public App() {
            InitializeComponent();

            ClientAnalyticsSession.Default.Start("fe61b1d3-9204-4a03-82d3-87873f05c888");
        }

    }
}