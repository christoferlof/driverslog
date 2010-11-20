using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_deleting_trip_spec : DeleteTripContext {
        
        [Fact]
        public void should_delete_current_trip() {
            Assert.Equal(0,Trip.All.Count);
        }

        [Fact]
        public void should_navigate_back_to_main() {
            Assert.Equal("/MainPage.xaml", NavigationService.NavigateUri.OriginalString);
        }

        [Fact]
        public void should_ask_for_confirmation() {
            Assert.True(MessageBoxService.ConfirmWasInvoked);
        }
    }
}