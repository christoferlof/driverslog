using System;
using System.Diagnostics;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Helpers;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class EditViewModel : TripScreen {
        private readonly INavigationService _navigationService;
        private Trip _trip;

        public EditViewModel(INavigationService navigationService, IMessageBoxService messageBoxService, IAnalyticsService analyticsService) 
            :base (messageBoxService, null, analyticsService){
            _navigationService = navigationService;
        }

        public string TripId { get; set; }

        protected override void OnInitialize() {
            base.OnInitialize();
            
            var id = new Guid(TripId);
            _trip = Trip.All.Single(x => x.Id == id);
            
            From            = _trip.From;
            To              = _trip.To;
            OdometerStart   = _trip.OdometerStart.AsString();
            OdometerStop    = _trip.OdometerStop.AsString();
            Notes           = _trip.Notes;
            Car             = _trip.Car;
            Date            = _trip.Date;
            Mileage         = _trip.Mileage.AsString();
            IsRoundtrip     = _trip.IsRoundTrip;
        }

        public void UpdateTrip() {
            _trip.From          = From;
            _trip.Notes         = Notes;
            _trip.OdometerStart = OdometerStart.AsInt();
            _trip.OdometerStop  = OdometerStop.AsInt();
            _trip.Car           = Car;
            _trip.To            = To;
            _trip.Date          = Date;
            _trip.Mileage       = Mileage.AsInt();
            _trip.IsRoundTrip   = IsRoundtrip;

            if(!IsValid(_trip)) return;

            Trip.SaveChanges();

            Location.AddIfNew(_trip);
            Location.SaveChanges();

            AnalyticsService.LogEvent("Trip.Edit");
            NavigateToMain();
        }

        private void NavigateToMain() {
            _navigationService.GoBack();
        }

        public void DeleteTrip() {
            if(!MessageBoxService.Confirm(Strings.EditTripDeleteConfirm)) return;
            Trip.All.Remove(_trip);
            Trip.SaveChanges();
            NavigateToMain();
        }
    }
}