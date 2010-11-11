using System;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_selecting_existing_trip_spec {
        
        [Fact]
        public void should_navigate_to_edit_page() {
            
            var navigationService = new NavigationServiceStub();
            var viewModel = new MainPageViewModel(navigationService);

            var id = new Guid("E0F6658A-A510-4864-A9AA-E79EB06B3E0D");
            viewModel.SelectedTrip  = new Trip{Id = id};

            Assert.Equal("/EditView.xaml?TripId="+id, navigationService.NavigateUri.OriginalString);

        }

    }
}