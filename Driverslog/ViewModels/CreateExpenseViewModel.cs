using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class CreateExpenseViewModel : ExpenseScreen {
        private readonly INavigationService _navigationService;

        public CreateExpenseViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) 
            : base(messageBoxService){
            _navigationService = navigationService;
        }

        public void CreateExpense() {
            var expense = new Expense {
                Car     = Car,
                Date    = Date,
                Title   = Title,
                Notes   = Notes,
                Amount  = Amount
            };

            if (!IsValid(expense)) return;

            Expense.Add(expense);
            Expense.SaveChanges();

            _navigationService.GoBack();
        }
    }
}