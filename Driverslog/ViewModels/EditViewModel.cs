using System;
using System.Diagnostics;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;

namespace Driverslog.ViewModels {
    public class EditViewModel : TripScreen {
        private INavigationService _navigationService;
        private Trip _trip;

        public EditViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
        }

        public string TripId { get; set; }

        protected override void OnInitialize() {
            var id = new Guid(TripId);
            _trip = Trip.All.Single(x => x.Id == id);

            From = _trip.From;
            To = _trip.To;
            OdometerStart = _trip.OdometerStart;
            OdometerStop = _trip.OdometerStop;
            Notes = _trip.Notes;
            Car = _trip.Car;
        }

        public void UpdateTrip() {
            _trip.From = From;
            _trip.Notes = Notes;
            _trip.OdometerStart = OdometerStart;
            _trip.OdometerStop = OdometerStop;
            _trip.Car = Car;
            _trip.To = To;

            Trip.SaveChanges();

            _navigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
        }
    }
}