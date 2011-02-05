using Driverslog.Models;
using Driverslog.Services;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_deleting_expense_spec : DeleteExpenseContext {
        
        [Fact]
        public void should_delete_current_trip() {
            Assert.Equal(0, Expense.All.Count);
        }

        [Fact]
        public void should_navigate_back_to_main() {
            Assert.True(NavigationService.GoBackWasCalled);
        }

        [Fact]
        public void should_ask_for_confirmation() {
            Assert.True(MessageBoxService.ConfirmWasInvoked);
        }
    }
}