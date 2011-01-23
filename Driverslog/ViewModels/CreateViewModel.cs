using System;
using System.Linq;
using Caliburn.Micro;
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
                To              = To,
                Date            = Date
            };
            
            if (!trip.IsValid()) {
                _messageBoxService.ShowMessage(
                    string.Join("\n",trip.ValidationMessages.Select(x => x.Value).ToArray()));
                return;
            }

            Trip.Add(trip);     
            Trip.SaveChanges();
            
            _navigationService.GoBack();
        }
    }
}
