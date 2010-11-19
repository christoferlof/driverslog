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
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class CreateViewModel : TripScreen {
        private readonly INavigationService _navigationService;
        private readonly IMessageBoxService _messageBoxService;

        public CreateViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) {
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
        }

        public void CreateTrip() {
            var trip = new Trip {
                Car             = Car,
                From            = From,
                Notes           = Notes,
                OdometerStart   = OdometerStart,
                To              = To
            };
            
            if (!trip.IsValid()) {
                _messageBoxService.ShowMessage(
                    string.Join("\n",trip.ValidationMessages.Select(x => x.Value).ToArray()));
                return;
            }

            Trip.AddFirst(trip);     
            Trip.SaveChanges();
            
            _navigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}
