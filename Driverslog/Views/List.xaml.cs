﻿using System;
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

namespace Driverslog.Views {
    public partial class List : PhoneApplicationPage {
        public List() {
            InitializeComponent();
            DataContext = new ListViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            NavigationService.Navigate(new Uri("/views/CreateTrip.xaml", UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) {
            Trip.Load();
        }
    }
}