using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNew {
    public class when_validating_spec : ContextSpecification {

        protected MessageBoxServiceStub MessageBoxService;
        protected CreateViewModel ViewModel;

        public override void Context() {
            MessageBoxService = new MessageBoxServiceStub();
            ViewModel = new CreateViewModel(new NavigationServiceStub(), MessageBoxService, null);
        }
        
        public override void Because() {
            ViewModel.CreateTrip();
        }

        [Fact]
        public void should_display_message_if_not_valid() {   
            Assert.True(MessageBoxService.ShowMessageWasInvoked);
        }

    }
}
