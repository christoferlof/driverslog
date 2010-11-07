using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using Caliburn.Micro;
using Driverslog.Commands;
using Driverslog.Models;

namespace Driverslog.ViewModels {
    public class CreateViewModel {
        private readonly INavigationService _navigationService;

        public CreateViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
        }

        public string From { get; set; }

        public string To { get; set; }

        public int OdometerStart { get; set; }

        public int OdometerStop { get; set; }

        public int Distance { get; private set; }

        public string Car { get; set; }

        public string Notes { get; set; }

        public void SaveTrip() {
            Trip.All.Add(new Trip {
                Car             = Car,
                From            = From,
                Notes           = Notes,
                OdometerStart   = OdometerStart,
                OdometerStop    = OdometerStop,
                To              = To
            });     
            Trip.SaveChanges();
            _navigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}
