using System;
using System.ComponentModel;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_selecting_existing_trip_spec : ContextSpecification {

        protected NavigationServiceStub NavigationService;
        protected MainPageViewModel ViewModel;
        protected Guid Id;
        protected PropertyChangedEventArgs PropertyChangedEventArgs;

        public override void Context() {
            NavigationService = new NavigationServiceStub();
            ViewModel = new MainPageViewModel(NavigationService);
            Id = new Guid("E0F6658A-A510-4864-A9AA-E79EB06B3E0D");
            ViewModel.PropertyChanged += (s,e) => PropertyChangedEventArgs = e;
        }

        public override void Because() {
            ViewModel.SelectedTrip = new Trip { Id = Id };
            ViewModel.SelectedIndex = 1;
            ViewModel.EditTrip();
        }

        [Fact]
        public void should_navigate_to_edit_page() {
            Assert.Equal("/EditView.xaml?TripId="+Id, NavigationService.NavigateUri.OriginalString);
        }

        [Fact]
        public void should_clear_selected_index() {
            Assert.Equal(-1, ViewModel.SelectedIndex);
        }

        [Fact]
        public void should_notify_property_changed() {
            Assert.Equal("SelectedIndex",PropertyChangedEventArgs.PropertyName);
        }
    }
}