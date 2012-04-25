using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class DeleteExpenseContext : ContextSpecification {

        protected TestableEditExpenseViewModel  ViewModel;
        protected NavigationServiceStub         NavigationService;
        protected Expense                       CurrentExpense;
        protected MessageBoxServiceStub         MessageBoxService;
        protected AnalyticsServiceStub          AnalyticsService;

        public override void Context() {

            Expense.Clear();
            Expense.SaveChanges();
            CurrentExpense = new Expense();
            Expense.Add(CurrentExpense);

            NavigationService = new NavigationServiceStub();
            MessageBoxService = new MessageBoxServiceStub();
            AnalyticsService    = new AnalyticsServiceStub();
            ViewModel = new TestableEditExpenseViewModel(NavigationService, MessageBoxService, AnalyticsService);

            ViewModel.ExpenseId = CurrentExpense.Id.ToString();
        }

        public override void Because() {
            ViewModel.Initialize();
            ViewModel.DeleteExpense();
        }

    }
}