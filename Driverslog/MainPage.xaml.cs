using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Driverslog.Tests;
using Microsoft.Phone.Controls;

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

            //var testClass = new CreateTripTests {
            //    RootFrame = Application.Current.RootVisual as PhoneApplicationFrame
            //};

            //ThreadPool.QueueUserWorkItem(d => testClass.should_hold_to_and_car());
        }
    }
}