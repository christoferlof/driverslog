using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels {
    public class when_initializing_main_view_spec : ContextSpecification {

        protected MainPageViewModel PageViewModel;
        protected NavigationServiceStub NavigationService;

        public override void Context() {
            NavigationService = new NavigationServiceStub();
            Trip.All.Clear();
            Trip.Add(new Trip());
            Trip.SaveChanges();
        }

        public override void Because() {
            PageViewModel = new MainPageViewModel(NavigationService);
        }

        [Fact]
        public void should_list_all_existing_trips() {

            Assert.Equal(1, PageViewModel.TripList.Count);

        }

    }
}