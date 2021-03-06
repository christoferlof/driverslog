using System;
using System.Linq;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNewExpense {
    public class when_saving_expense_spec : ContextSpecification {

        protected CreateExpenseViewModel    ViewModel;
        protected NavigationServiceStub     NavigationService;
        protected MessageBoxServiceStub     MessageBoxService;
        protected AnalyticsServiceStub      AnalyticsService;

        public override void Context() {
            Expense.Clear();
            Expense.SaveChanges();

            NavigationService = new NavigationServiceStub();
            MessageBoxService = new MessageBoxServiceStub();
            AnalyticsService =  new AnalyticsServiceStub();

            ViewModel = new CreateExpenseViewModel(NavigationService, MessageBoxService, null, AnalyticsService) {
                Car     = "car",
                Date    = DateTime.Now,
                Amount  = 2.99,
                Notes   = "notes",
                Title   = "title"
            };
        }

        public override void Because() {
            ViewModel.CreateExpense();
        }

        [Fact]
        public void should_create_new_expense() {

            Expense.Load();

            Assert.Equal(1, Expense.All.Count);

            var expense = Expense.All.First();

            Assert.Equal(ViewModel.Car, expense.Car);
            Assert.Equal(ViewModel.Date.ToString(), expense.Date.ToString()); //todo: add a real Equals<date>
            Assert.Equal(ViewModel.Title, expense.Title);
            Assert.Equal(ViewModel.Notes, expense.Notes);
            Assert.Equal(ViewModel.Amount, expense.Amount);

        }

        [Fact]
        public void should_navigate_to_main() {
            Assert.True(NavigationService.GoBackWasCalled);
        }

        [Fact]
        public void should_not_display_validation_message() {
            Assert.Equal(false, MessageBoxService.ShowMessageWasInvoked);
        }

        [Fact]
        public void should_raise_create_event() {
            Assert.True(AnalyticsService.LogEventWasInvoked);
        }
    }
}