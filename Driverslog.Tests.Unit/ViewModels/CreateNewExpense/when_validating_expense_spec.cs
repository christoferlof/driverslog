using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNewExpense {
    public class when_validating_expense_spec : ContextSpecification {
        protected MessageBoxServiceStub MessageBoxService;
        protected CreateExpenseViewModel ViewModel;

        public override void Context() {
            MessageBoxService = new MessageBoxServiceStub();
            ViewModel = new CreateExpenseViewModel(new NavigationServiceStub(), MessageBoxService);
        }

        public override void Because() {
            ViewModel.CreateExpense();
        }

        [Fact]
        public void should_display_message_if_not_valid() {
            Assert.True(MessageBoxService.ShowMessageWasInvoked);
        }
    }
}