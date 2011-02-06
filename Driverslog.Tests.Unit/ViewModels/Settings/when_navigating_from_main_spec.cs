using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Settings {
    public class when_navigating_from_main_spec {
        
        [Fact]
        public void should_navigate_to_settings() {
            
            var navigationService = new NavigationServiceStub();
            var viewModel = new MainPageViewModel(navigationService);

            viewModel.Settings();

            Assert.Equal("/SettingsView.xaml", navigationService.NavigateUri.OriginalString);    
        }

        
    }
}