using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_updating_trip_spec : ContextSpecification {

        protected TestableEditViewModel ViewModel;
        protected NavigationServiceStub NavigationService;
        protected Trip CurrentTrip;
        protected MessageBoxServiceStub MessageBoxService;
        protected AnalyticsServiceStub AnalyticsService;

        public override void Context() {

            Trip.Clear();
            Trip.SaveChanges();
            CurrentTrip = new Trip();
            Trip.Add(CurrentTrip);

            NavigationService = new NavigationServiceStub();
            AnalyticsService = new AnalyticsServiceStub();
            ViewModel = new TestableEditViewModel(NavigationService, MessageBoxService, AnalyticsService);

            ViewModel.TripId = CurrentTrip.Id.ToString();
        }

        public override void Because() {
            ViewModel.Initialize();
            ViewModel.To = "To";
            ViewModel.From = "From";
            ViewModel.UpdateTrip();
        }

        [Fact]
        public void should_update_current_trip() {
            Assert.Equal("To",CurrentTrip.To);
            Assert.Equal("From",CurrentTrip.From);
        }

        [Fact]
        public void should_navigate_back_to_main() {
            Assert.True(NavigationService.GoBackWasCalled);
        }

        [Fact]
        public void should_log_update_event() {
            Assert.True(AnalyticsService.LogEventWasInvoked);
        }
    }
}