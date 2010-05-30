using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Json;
using System.Windows;
using Driverslog.Models;

namespace Driverslog.ViewModels {
    public class ListViewModel {

        public ListViewModel() {
            LoadTrips();
        }

        private void LoadTrips() {
            Trip.Load();
        }

        public ObservableCollection<Trip> TripList {
            get { return Trip.All; }
        }
    }
}