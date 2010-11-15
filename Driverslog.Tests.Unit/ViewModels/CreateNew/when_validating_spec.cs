using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNew {
    public class when_validating_spec {
        
        [Fact]
        public void should_display_message_if_not_valid() {
            var messageBoxService = new MessageBoxServiceStub();
            var viewModel = new CreateViewModel(new NavigationServiceStub(), messageBoxService);
            
            viewModel.CreateTrip();
            
            Assert.True(messageBoxService.ShowMessageWasInvoked);
        }
    }
}