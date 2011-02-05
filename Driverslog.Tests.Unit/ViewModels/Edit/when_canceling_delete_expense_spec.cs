using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_canceling_delete_expense_spec : DeleteExpenseContext{
        public override void Context() {
            base.Context();
            MessageBoxService.ConfirmReturns = false;
        }

        [Fact]
        public void should_not_delete_trip() {
            Assert.Equal(1, Expense.All.Count);
        }

        [Fact]
        public void should_not_navigate() {
            Assert.True(!NavigationService.GoBackWasCalled);
        }

    }
}