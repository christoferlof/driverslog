using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class EditExpenseViewModel : ExpenseScreen {
        private readonly INavigationService _navigationService;
        private Expense _expense;

        public EditExpenseViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) 
            : base(messageBoxService){
            _navigationService = navigationService;
        }

        protected override void OnInitialize() {
            base.OnInitialize();

            var id = new Guid(ExpenseId);
            _expense = Expense.All.Single(x => x.Id == id);

            Title = _expense.Title;
            Car = _expense.Car;
            Amount = _expense.Amount;
            Date = _expense.Date;
            Notes = _expense.Notes;
        }

        public string ExpenseId { get; set; }

        public void UpdateExpense() {
            _expense.Title = Title;
            _expense.Car = Car;
            _expense.Amount = Amount;
            _expense.Date = Date;
            _expense.Notes = Notes;

            if (!IsValid(_expense)) return;

            Expense.SaveChanges();
            _navigationService.GoBack();
        }

        public void DeleteExpense() {
            if (!MessageBoxService.Confirm("Do you really wan't to delete this expense?")) return;
            Expense.All.Remove(_expense);
            Expense.SaveChanges();
            NavigateToMain();
        }

        private void NavigateToMain() {
            _navigationService.GoBack();
        }
    }
}