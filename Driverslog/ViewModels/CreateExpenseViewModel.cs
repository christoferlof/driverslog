using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class CreateExpenseViewModel {
        private readonly INavigationService _navigationService;
        private readonly IMessageBoxService _messageBoxService;

        public CreateExpenseViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) {
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
        }

        public void CreateExpense() {
            var expense = new Expense {
                Car     = Car,
                Date    = Date,
                Title   = Title,
                Notes   = Notes,
                Amount  = Amount
            };

            if (!expense.IsValid()) {
                _messageBoxService.ShowMessage(
                    string.Join("\n", expense.ValidationMessages.Select(x => x.Value).ToArray()));
                return;
            }

            Expense.Add(expense);
            Expense.SaveChanges();

            _navigationService.Navigate(new Uri("/MainPage.xaml",UriKind.Relative));
        }

        public string Car { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public string Notes { get; set; }

        public double Amount { get; set; }
    }
}