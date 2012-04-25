using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Driverslog.Models;
using Driverslog.Services;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNew {
    public class when_saving_trip_trial_mode_limit_reached_spec : ContextSpecification {
        
        protected CreateViewModel       ViewModel;
        protected NavigationServiceStub NavigationService;
        protected MessageBoxServiceStub MessageBoxService;
        protected TrialServiceStub      TrialService;
        protected AnalyticsServiceStub  AnalyticsService;

        public override void Context() {
            TrialService = new TrialServiceStub {Limit = 5};
            Trip.Clear();
            
            //10 items limit
            for(var i=0;i<TrialService.Limit;i++)
                Trip.Add(new Trip());

            Trip.SaveChanges();

            NavigationService = new NavigationServiceStub();
            MessageBoxService = new MessageBoxServiceStub();
            AnalyticsService  = new AnalyticsServiceStub();
            ViewModel = new CreateViewModel(NavigationService, MessageBoxService, TrialService, AnalyticsService) {
                Car = "car",
                From = "from",
                Notes = "notes",
                OdometerStart = 1,
                To = "to",
                Date = new DateTime(1979, 1, 3)

            };

        }

        public override void Because() {
            ViewModel.CreateTrip();
        }

        [Fact]
        public void should_not_save_trip() {
          Assert.Equal(TrialService.Limit, Trip.All.Count);
        }

        [Fact]
        public void should_notify_user() {
            Assert.True(MessageBoxService.ConfirmWasInvoked);
        }

    }
}
