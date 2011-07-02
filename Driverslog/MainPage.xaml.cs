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
using Driverslog.Models;
using Driverslog.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Marketplace;

namespace Driverslog {
    public partial class MainPage : PhoneApplicationPage {
        public MainPage() {
            InitializeComponent();   
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
    }
}