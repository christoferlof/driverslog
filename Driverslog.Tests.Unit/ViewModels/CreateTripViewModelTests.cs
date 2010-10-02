using System.Linq;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels {
    public class CreateTripViewModelTests {

        public CreateTripViewModelTests() {
            Trip.Clear();
            Trip.SaveChanges();
        }

        [Fact]
        public void should_create_new_trip_on_save() {

            var viewModel = new CreateTripViewModel(new NavigationServiceStub()) {
                Car             = "car",
                From            = "from",
                Notes           = "notes",
                OdometerStart   = 1,
                OdometerStop    = 2,
                To              = "to"
            };

            viewModel.SaveTripCommand.Execute(false);
            
            Trip.Load();

            Assert.Equal(1, Trip.All.Count);

            var trip = Trip.All.First();
            Assert.Equal(viewModel.Car, trip.Car);
            Assert.Equal(viewModel.From, trip.From);
            Assert.Equal(viewModel.Notes, trip.Notes);
            Assert.Equal(viewModel.OdometerStart, trip.OdometerStart);
            Assert.Equal(viewModel.OdometerStop, trip.OdometerStop);
            Assert.Equal(viewModel.To, trip.To);
        }
    }
}