using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Input;
using Driverslog.Commands;
using Driverslog.Models;
using Microsoft.Phone.Tasks;

namespace Driverslog.ViewModels {
    public class ListViewModel {
        
        public ListViewModel() {
            ExportTripsCommand = new DelegateCommand(ExportTrips);
            Trip.Load();
        }

        public ObservableCollection<Trip> TripList {
            get { return Trip.All; }
        }

        public ICommand ExportTripsCommand { get; private set; }

        private static void ExportTrips(object obj) {

            var body = Trip.All.Aggregate(string.Empty, (current, trip) => current + FormatTrip(trip));
            var subject = "My drive log";
            var to = Setting.Current.Email;

            var mail = new EmailComposeTask {Body = body, Subject = subject, To = to};
            mail.Show();
        }

        private static string FormatTrip(Trip trip) {
            return trip.Car + ";" + 
                trip.From + ";" + 
                trip.To + ";" + 
                trip.OdometerStart + ";" + 
                trip.OdometerStop + ";" + 
                trip.Notes + "\n";
        }
    }
}