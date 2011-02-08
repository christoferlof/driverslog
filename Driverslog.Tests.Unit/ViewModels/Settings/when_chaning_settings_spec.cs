using System;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Settings {
    public class when_chaning_settings_spec : ContextSpecification {

        protected NavigationServiceStub NavigationService;
        protected SettingsViewModel ViewModel;

        public override void Context() {

            NavigationService = new NavigationServiceStub();
            ViewModel = new SettingsViewModel(NavigationService) {
                Car     = "car",
                Email   = "email",
                DistanceUnit = "km"
            };
        }

        public override void Because() {
            ViewModel.SaveSettings();
        }

        [Fact]
        public void should_save_settings() {
            Assert.Equal(Setting.Current.DefaultCar,ViewModel.Car);
            Assert.Equal(Setting.Current.DistanceUnit,ViewModel.DistanceUnit);
            Assert.Equal(Setting.Current.Email,ViewModel.Email);
        }

        [Fact]
        public void should_navigate_back() {
            Assert.True(NavigationService.GoBackWasCalled);
        }
    }
}