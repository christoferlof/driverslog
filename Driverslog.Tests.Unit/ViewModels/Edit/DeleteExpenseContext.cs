using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class DeleteExpenseContext : ContextSpecification {

        protected TestableEditExpenseViewModel ViewModel;
        protected NavigationServiceStub NavigationService;
        protected Expense CurrentExpense;
        protected MessageBoxServiceStub MessageBoxService;

        public override void Context() {

            Expense.Clear();
            Expense.SaveChanges();
            CurrentExpense = new Expense();
            Expense.Add(CurrentExpense);

            NavigationService = new NavigationServiceStub();
            MessageBoxService = new MessageBoxServiceStub();
            ViewModel = new TestableEditExpenseViewModel(NavigationService, MessageBoxService);

            ViewModel.ExpenseId = CurrentExpense.Id.ToString();
        }

        public override void Because() {
            ViewModel.Initialize();
            ViewModel.DeleteExpense();
        }

    }
}