using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Helpers;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class CreateViewModel : TripScreen {
        private readonly INavigationService _navigationService;

        public CreateViewModel(INavigationService navigationService,
            IMessageBoxService messageBoxService, ITrialService trialService, IAnalyticsService analyticsService)
            : base(messageBoxService, trialService, analyticsService) {
            _navigationService = navigationService;
        }

        public void CreateTrip() {
            
            if(IsTrialLimitReached()) return;
            
            var trip          = new Trip {
                Car           = Car,
                From          = From,
                Notes         = Notes,
                OdometerStart = OdometerStart.AsInt(),
                OdometerStop  = OdometerStop.AsInt(),
                To            = To,
                Date          = Date,
                Mileage       = Mileage.AsInt(),
                IsRoundTrip   = IsRoundtrip
            };

            if (!IsValid(trip)) return;

            Trip.Add(trip);
            Trip.SaveChanges();

            AnalyticsService.LogEvent("Trip.Create");
            _navigationService.GoBack();
        }
    }
}
