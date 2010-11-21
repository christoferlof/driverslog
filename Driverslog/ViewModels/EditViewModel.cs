using System;
using System.Diagnostics;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class EditViewModel : TripScreen {
        private INavigationService _navigationService;
        private readonly IMessageBoxService _messageBoxService;
        private Trip _trip;

        public EditViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) {
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
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

            Trip.SaveChanges();
            NavigateToMain();
        }

        private void NavigateToMain() {
            _navigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
        }

        public void DeleteTrip() {
            if(!_messageBoxService.Confirm("Do you really wan't to delete this trip?")) return;
            Trip.All.Remove(_trip);
            Trip.SaveChanges();
            NavigateToMain();
        }
    }
}