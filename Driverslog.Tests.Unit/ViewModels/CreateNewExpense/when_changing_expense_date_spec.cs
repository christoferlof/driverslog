using System;
using Driverslog.ViewModels;
using Microsoft.Phone.Controls;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.CreateNewExpense {
    public class when_changing_expense_date_spec {

        [Fact]
        public void should_update_date() {
            var viewModel = new CreateExpenseViewModel(new NavigationServiceStub(), new MessageBoxServiceStub());
            var date = new DateTime(1979, 1, 3);

            viewModel.ChangeDate(new DateTimeValueChangedEventArgs(null, date));

            Assert.Equal(date, viewModel.Date);
        }

    }
}