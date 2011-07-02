using System;
using System.Linq;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNew {
    public class when_saving_trip_spec : ContextSpecification {
        
        protected CreateViewModel   ViewModel;
        protected NavigationServiceStub NavigationService;
        protected MessageBoxServiceStub MessageBoxService;

        public override void Context() {
            Trip.Clear();
            Trip.SaveChanges();

            NavigationService = new NavigationServiceStub();
            MessageBoxService = new MessageBoxServiceStub();
            ViewModel = new CreateViewModel(NavigationService, MessageBoxService, null) {
                Car             = "car",
                From            = "from",
                Notes           = "notes",
                OdometerStart   = 1,
                To              = "to",
                Date            = new DateTime(1979,1,3)
                
            };

        }

        public override void Because() {
            ViewModel.CreateTrip();
        }

        [Fact]
        public void should_create_new_trip() {

            Trip.Load();

            Assert.Equal(1, Trip.All.Count);

            var trip = Trip.All.First();
            Assert.Equal(ViewModel.Car, trip.Car);
            Assert.Equal(ViewModel.From, trip.From);
            Assert.Equal(ViewModel.Notes, trip.Notes);
            Assert.Equal(ViewModel.OdometerStart, trip.OdometerStart);
            Assert.Equal(ViewModel.To, trip.To);
            Assert.Equal(ViewModel.Date, trip.Date);
        }

        [Fact]
        public void should_navigate_to_main() {
            Assert.True(NavigationService.GoBackWasCalled);
        }

        [Fact]
        public void should_not_display_validation_message() {
            Assert.Equal(false,MessageBoxService.ShowMessageWasInvoked);
        }
    }
}