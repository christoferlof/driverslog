using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Marketplace;
using Microsoft.Phone.Shell;

namespace Driverslog {
    public partial class MainPage : PhoneApplicationPage {
        public MainPage() {
            InitializeComponent();
            CreateApplicationBar();
        }

        private void TodayViewSourceFilter(object sender, FilterEventArgs e) {
            if (e.Item is Expense) {
                e.Accepted = ((Expense)e.Item).Date.Date == DateTime.Today.Date;
            }
            if (e.Item is Trip) {
                e.Accepted = ((Trip)e.Item).Date.Date == DateTime.Today.Date;
            }
        }

        private void TripViewSourceFilter(object sender, FilterEventArgs e) {
            e.Accepted = e.Item is Trip;
        }

        private void ExpenseViewSourceFilter(object sender, FilterEventArgs e) {
            e.Accepted = e.Item is Expense;
        }

        private void CreateApplicationBar() {
            ApplicationBar = new ApplicationBar();

            ApplicationBar.Buttons.Add(new AppBarButton {
                IconUri = new Uri("/icons/appbar.newtrip.png", UriKind.Relative),
                Message = "CreateNewTrip",
                Text    = Strings.MainPageAppBarTrip
            });

            ApplicationBar.Buttons.Add(new AppBarButton {
                IconUri = new Uri("/icons/appbar.newexpense.png", UriKind.Relative),
                Message = "CreateNewExpense",
                Text    = Strings.MainPageAppBarExpense
            });

            ApplicationBar.MenuItems.Add(new AppBarMenuItem {
                Message = "ExportTrips",
                Text    = Strings.MainPageAppBarExport
            });

            ApplicationBar.MenuItems.Add(new AppBarMenuItem {
                Message = "Clear",
                Text    = Strings.MainPageAppBarClear
            });

            ApplicationBar.MenuItems.Add(new AppBarMenuItem {
                Message = "Settings",
                Text = Strings.MainPageAppBarSettings
            });

        }
    }
}