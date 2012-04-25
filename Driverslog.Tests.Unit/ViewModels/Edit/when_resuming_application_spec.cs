using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_resuming_application_spec {

        [Fact]
        public void should_load_trips_if_not_loaded() {
            Trip.Clear();
            Trip.SaveChanges();

            Trip.Load();
            var trip = new Trip();
            Trip.Add(trip);
            Trip.SaveChanges();
            Trip.Clear();

            var viewModel = new TestableEditViewModel() {
                TripId = trip.Id.ToString()
            };
            viewModel.Initialize();

            Assert.Equal(1,Trip.All.Count);
        }

    }
}