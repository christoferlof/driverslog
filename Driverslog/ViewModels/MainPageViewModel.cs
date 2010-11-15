using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Driverslog.Commands;
using Driverslog.Helpers;
using Driverslog.Models;
using Microsoft.Phone.Tasks;

namespace Driverslog.ViewModels {
    public class MainPageViewModel : Screen {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
            _selectedIndex = -1;
            Trip.Load();
        }

        public IEnumerable<Trip> TripList {
            get { return Trip.ByDateDescending(); }
        }

        public Trip SelectedTrip { get; set; }

        private int _selectedIndex;
        public int SelectedIndex {
            get { return _selectedIndex; }
            set {
                _selectedIndex = value;
                NotifyOfPropertyChange(() => SelectedIndex);
            }
        }

        public void EditTrip() {
            if (SelectedIndex == -1) return;
            _navigationService.Navigate(new Uri("/EditView.xaml?TripId=" + SelectedTrip.Id, UriKind.Relative));
            SelectedIndex = -1;
        }

        public void CreateNewTrip() {
            _navigationService.Navigate(new Uri("/CreateView.xaml", UriKind.Relative));
        }

        public void ExportTrips() {
            MessageBox.Show(EmailHelper.Format(Trip.ByDateDescending()));
            var task = new EmailComposeTask() {
                Subject = "Your drive log",
                Body = EmailHelper.Format(Trip.ByDateDescending())
            };
            task.Show();
        }
    }
}