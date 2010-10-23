using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using Driverslog.Tests;
using Microsoft.Phone.Controls;
using Victoria.Test.Runner;

namespace Driverslog {
    public partial class MainPage : PhoneApplicationPage {
        public MainPage() {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
        }

        private void button1_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("/views/List.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e) {
#if INTEGRATIONTEST

            var runner = new IntegrationTestRunner(
                new TestMethodResolver(
                    new StaticAssemblyResolver(GetType())),
                    new DebugOutputWriter(),
                    (PhoneApplicationFrame)Application.Current.RootVisual);
            
            runner.Execute(string.Empty);

            //var testClass = new CreateTripTests {
            //    RootFrame = Application.Current.RootVisual as PhoneApplicationFrame
            //};

            //var handle = new AutoResetEvent(false);

            //ThreadPool.QueueUserWorkItem(d => {
            //    Debug.WriteLine("test 1 start");
            //    testClass.should_hold_to_and_car();
            //    handle.Set();
            //    Debug.WriteLine("test 1 end");
            //});
            
            //handle.WaitOne();

            //ThreadPool.QueueUserWorkItem(d => {
            //    Debug.WriteLine("test 2 start");
            //    testClass.should_hold_to_and_car();
            //    Debug.WriteLine("test 2 end");
            //});
#endif
        }
    }
}