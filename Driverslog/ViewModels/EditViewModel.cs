using System;
using System.Diagnostics;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class EditViewModel : TripScreen {
        private readonly INavigationService _navigationService;
        private Trip _trip;

        public EditViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) 
            :base (messageBoxService, null){
            _navigationService = navigationService;
        }

        public string TripId { get; set; }

        protected override void OnInitialize() {
            base.OnInitialize();
            
            var id = new Guid(TripId);
            _trip = Trip.All.Single(x => x.Id == id);
            
            From = _trip.From;
            To = _trip.To;
            OdometerStart = _trip.OdometerStart;
            OdometerStop = _trip.OdometerStop;
            Notes = _trip.Notes;
            Car = _trip.Car;
            Date = _trip.Date;
        }

        public void UpdateTrip() {
            _trip.From = From;
            _trip.Notes = Notes;
            _trip.OdometerStart = OdometerStart;
            _trip.OdometerStop = OdometerStop;
            _trip.Car = Car;
            _trip.To = To;
            _trip.Date = Date;

            if(!IsValid(_trip)) return;

            Trip.SaveChanges();
            NavigateToMain();
        }

        private void NavigateToMain() {
            _navigationService.GoBack();
        }

        public void DeleteTrip() {
            if(!MessageBoxService.Confirm("Do you really wan't to delete this trip?")) return;
            Trip.All.Remove(_trip);
            Trip.SaveChanges();
            NavigateToMain();
        }
    }
}