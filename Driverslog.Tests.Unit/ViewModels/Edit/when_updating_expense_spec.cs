using System;
using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_updating_expense_spec : ContextSpecification {

        protected TestableEditExpenseViewModel ViewModel;
        protected NavigationServiceStub NavigationService;
        protected Expense CurrentExpense;
        protected MessageBoxServiceStub MessageBoxService;
        protected AnalyticsServiceStub AnalyticsService;

        public override void Context() {

            Expense.Clear();
            Expense.SaveChanges();
            CurrentExpense = new Expense();
            Expense.Add(CurrentExpense);

            NavigationService   = new NavigationServiceStub();
            AnalyticsService    = new AnalyticsServiceStub();
            ViewModel           = new TestableEditExpenseViewModel(NavigationService, MessageBoxService, AnalyticsService);

            ViewModel.ExpenseId = CurrentExpense.Id.ToString();
        }

        public override void Because() {
            ViewModel.Initialize();
            ViewModel.Title = "Title";
            ViewModel.Car = "car";
            ViewModel.Date = DateTime.Now.Date;
            ViewModel.Amount = 1.0;
            ViewModel.Notes = "notes";
            ViewModel.UpdateExpense();
        }

        [Fact]
        public void should_update_current_expense() {
            Assert.Equal("Title", CurrentExpense.Title);
            Assert.Equal(1.0, CurrentExpense.Amount);
            Assert.Equal("car",CurrentExpense.Car);
            Assert.True(CurrentExpense.Date != default(DateTime));
            Assert.Equal("notes", CurrentExpense.Notes);
        }

        [Fact]
        public void should_navigate_back_to_main() {
            Assert.True(NavigationService.GoBackWasCalled);
        }

        [Fact]
        public void shoud_log_update_event() {
            Assert.True(AnalyticsService.LogEventWasInvoked);
        }
    }
}