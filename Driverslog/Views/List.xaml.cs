using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Driverslog.Models;
using Driverslog.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace Driverslog.Views {
    public partial class List : PhoneApplicationPage {
        public List() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("/views/CreateTrip.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Trip.Load();
        }

        private void Export_Click(object sender, EventArgs e) { 
            //since the applicationbaritem isn't a dependencyobject I can't attach a command to it.
            ((ListViewModel)DataContext).ExportTripsCommand.Execute(null);
        }

        private void Call_Click(object sender, EventArgs e) {
            var phone = new PhoneCallTask {DisplayName = "chris", PhoneNumber = "087249311"};    
            phone.Show();
        }

        private void CreateSettings_Click(object sender, EventArgs e) {
            Setting.All.Add(new Setting{Email = "christofer.lof@gmail.com"});
            Setting.SaveChanges();
        }
    }
}