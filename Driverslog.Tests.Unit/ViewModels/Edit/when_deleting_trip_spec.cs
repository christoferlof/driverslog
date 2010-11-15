using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_deleting_trip_spec : ContextSpecification {
        
        protected TestableEditViewModel ViewModel;
        protected NavigationServiceStub NavigationService;
        protected Trip CurrentTrip;

        public override void Context() {

            Trip.Clear();
            Trip.SaveChanges();
            CurrentTrip = new Trip();
            Trip.Add(CurrentTrip);

            NavigationService = new NavigationServiceStub();
            ViewModel = new TestableEditViewModel(NavigationService);

            ViewModel.TripId = CurrentTrip.Id.ToString();
        }

        public override void Because() {
            ViewModel.Initialize();
            ViewModel.DeleteTrip();
        }

        [Fact]
        public void should_delete_current_trip() {
            Assert.Equal(0,Trip.All.Count);
        }

        [Fact]
        public void should_navigate_back_to_main() {
            Assert.Equal("/MainPage.xaml", NavigationService.NavigateUri.OriginalString);
        }
    }
}