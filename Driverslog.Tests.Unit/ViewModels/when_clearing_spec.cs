using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels {
    public class when_clearing_spec {
        
        [Fact]
        public void should_confirm_clearing() {
            var messageBoxService = new MessageBoxServiceStub();
            var viewModel = new MainPageViewModel(null,messageBoxService, new AnalyticsServiceStub());
            viewModel.Clear();
            Assert.True(messageBoxService.ConfirmWasInvoked);
        }


        [Fact]
        public void should_only_clear_if_confirmed() {
            Trip.Add(new Trip());
            Expense.Add(new Expense());
            
            var messageBoxService = new MessageBoxServiceStub();
            messageBoxService.ConfirmReturns = true;
            var viewModel = new MainPageViewModel(null, messageBoxService, new AnalyticsServiceStub());
            viewModel.Clear();

            Assert.Equal(0,Trip.All.Count);
            Assert.Equal(0,Expense.All.Count);
        }
    }
}