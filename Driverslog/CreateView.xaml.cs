﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Caliburn.Micro;
using Driverslog.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Driverslog {
    using Driverslog.Models;

    public partial class CreateView : PhoneApplicationPage {
        public CreateView() {
            InitializeComponent();
            CreateApplicationBar();

            From.SuggestionsSource = Location.All;
            From.FilterKeyPath = "Name";
            To.SuggestionsSource = Location.All;
            To.FilterKeyPath = "Name";
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e) {
            ((CreateViewModel)DataContext).IsRoundtrip = true;
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e) {
            ((CreateViewModel)DataContext).IsRoundtrip = false;
        }

        private void CreateApplicationBar() {
            ApplicationBar = new ApplicationBar();
            ApplicationBar.BackgroundColor = (Color)Application.Current.Resources["AppDarkAccentColor"];
            ApplicationBar.ForegroundColor = (Color)Application.Current.Resources["AppForegroundColor"];

            ApplicationBar.Buttons.Add(new AppBarButton {
                IconUri = new Uri("/icons/appbar.check.rest.png", UriKind.Relative),
                Message = "CreateTrip",
                Text = Strings.TripSave
            });

        }

    }
}