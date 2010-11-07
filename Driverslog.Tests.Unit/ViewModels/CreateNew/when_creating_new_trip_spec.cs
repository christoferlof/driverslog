using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNew {
    public class when_creating_new_trip_spec : ContextSpecification {

        protected MainPageViewModel PageViewModel;
        protected NavigationServiceStub NavigationService;

        public override void Context() {
            NavigationService = new NavigationServiceStub();
            PageViewModel = new MainPageViewModel(NavigationService);
        }

        public override void Because() {
            PageViewModel.CreateNewTrip();
        }

        [Fact]
        public void should_navigate_to_create_page() {
            Assert.True(NavigationService.NavigateUri.OriginalString.Contains("CreateView.xaml"));
        }

    }
}