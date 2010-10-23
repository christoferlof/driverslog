using System.Linq;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels {
    public class when_saving_trip_spec : ContextSpecification {
        
        protected CreateTripViewModel   ViewModel;
        protected NavigationServiceStub NavigationService;

        public override void Context() {
            Trip.Clear();
            Trip.SaveChanges();

            NavigationService = new NavigationServiceStub();
            ViewModel = new CreateTripViewModel(NavigationService) {
                Car             = "car",
                From            = "from",
                Notes           = "notes",
                OdometerStart   = 1,
                OdometerStop    = 2,
                To              = "to"
            };

        }

        public override void Because() {
            ViewModel.SaveTripCommand.Execute(false);
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
            Assert.Equal(ViewModel.OdometerStop, trip.OdometerStop);
            Assert.Equal(ViewModel.To, trip.To);
        }

        [Fact]
        public void should_navigate_to_list() {
            Assert.True(NavigationService.NavigateUri.OriginalString.Contains("List.xaml"));
        }
    }
}