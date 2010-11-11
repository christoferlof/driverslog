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
using Driverslog.Models;
using Microsoft.Phone.Tasks;

namespace Driverslog.ViewModels {
    public class MainPageViewModel {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
            Trip.Load();
        }

        public ObservableCollection<Trip> TripList {
            get { return Trip.All; }
        }

        private Trip _selectedTrip;

        public Trip SelectedTrip {
            get { return _selectedTrip; }
            set {
                _selectedTrip = value;
                EditTrip(_selectedTrip);
            }
        }

        private void EditTrip(Trip selectedTrip) {
            _navigationService.Navigate(new Uri("/EditView.xaml?TripId="+selectedTrip.Id, UriKind.Relative));
        }

        public void CreateNewTrip() {
            _navigationService.Navigate(new Uri("/CreateView.xaml",UriKind.Relative));
        }
    }
}