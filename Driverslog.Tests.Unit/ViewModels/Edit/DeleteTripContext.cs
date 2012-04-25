using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class DeleteTripContext : ContextSpecification {

        protected TestableEditViewModel ViewModel;
        protected NavigationServiceStub NavigationService;
        protected Trip                  CurrentTrip;
        protected MessageBoxServiceStub MessageBoxService;
        protected AnalyticsServiceStub  AnalyticsService;

        public override void Context() {

            Trip.Clear();
            Trip.SaveChanges();
            CurrentTrip = new Trip();
            Trip.Add(CurrentTrip);

            NavigationService = new NavigationServiceStub();
            MessageBoxService = new MessageBoxServiceStub();
            AnalyticsService =  new AnalyticsServiceStub();
            ViewModel = new TestableEditViewModel(NavigationService, MessageBoxService, AnalyticsService);

            ViewModel.TripId = CurrentTrip.Id.ToString();
        }

        public override void Because() {
            ViewModel.Initialize();
            ViewModel.DeleteTrip();
        }
    }
}