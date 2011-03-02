using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_updating_amount_on_expense_spec {
        
        [Fact]
        public void should_not_fire_notify_if_amount_is_same() {
            var viewModel = new TestableEditExpenseViewModel(null,null);
            bool fired = false;
            viewModel.Amount = 1.0;
            viewModel.PropertyChanged += (s,e) => fired = true;

            viewModel.Amount = 1.0;

            Assert.True(!fired);
        }

        [Fact]
        public void should_fire_notify_if_amount_is_different() {
            var viewModel = new TestableEditExpenseViewModel(null, null);
            bool fired = false;
            viewModel.Amount = 1.0;
            viewModel.PropertyChanged += (s, e) => fired = true;

            viewModel.Amount = 1.1;

            Assert.True(fired);
        }

    }
}