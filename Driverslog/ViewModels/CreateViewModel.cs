using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class CreateViewModel : TripScreen {
        private readonly INavigationService _navigationService;

        public CreateViewModel(INavigationService navigationService, 
            IMessageBoxService messageBoxService, ITrialService trialService)
            : base(messageBoxService, trialService) {
            _navigationService = navigationService;
        }

        public void CreateTrip() {
            
            if(IsTrialLimitReached()) return;
            
            var trip = new Trip {
                Car = Car,
                From = From,
                Notes = Notes,
                OdometerStart = OdometerStart,
                To = To,
                Date = Date
            };

            if (!IsValid(trip)) return;

            Trip.Add(trip);
            Trip.SaveChanges();

            _navigationService.GoBack();
        }
    }
}
