using System;
using System.ComponentModel;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels.Edit {
    public class when_selecting_existing_expense_spec : ContextSpecification {


        protected NavigationServiceStub NavigationService;
        protected MainPageViewModel ViewModel;
        protected Guid Id;

        public override void Context() {
            NavigationService = new NavigationServiceStub();
            ViewModel = new MainPageViewModel(NavigationService);
            Id = new Guid("E0F6658A-A510-4864-A9AA-E79EB06B3E0D");
        }

        public override void Because() {
            ViewModel.SelectedItem = new Expense { Id = Id };
            ViewModel.SelectedIndex = 1;
            ViewModel.EditItem();
        }

        [Fact]
        public void should_navigate_to_edit_expense_page() {
            Assert.Equal("/EditExpenseView.xaml?ExpenseId=" + Id, NavigationService.NavigateUri.OriginalString);
        }

        [Fact]
        public void should_clear_selected_index() {
            Assert.Equal(-1, ViewModel.SelectedIndex);
        }

    }
}