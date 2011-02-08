using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Driverslog.Helpers;
using Driverslog.Models;
using Driverslog.Services;
using Microsoft.Phone.Tasks;

namespace Driverslog.ViewModels {
    public class MainPageViewModel : Screen {
        private readonly INavigationService _navigationService;
        private readonly IMessageBoxService _messageBoxService;

        public MainPageViewModel(INavigationService navigationService, IMessageBoxService messageBoxService) {
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
            SelectedIndex = -1;
        }

        protected override void OnInitialize() {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                Trip.Load();
                Expense.Load();
            });
        }

        public ObservableCollection<Trip> TripList {
            get { return Trip.All; }
        }

        public ObservableCollection<Expense> ExpenseList {
            get { return Expense.All; }
        }

        public IHaveId SelectedItem { get; set; }

        public int SelectedIndex { get; set; }

        public void CreateNewTrip() {
            _navigationService.Navigate(new Uri("/CreateView.xaml", UriKind.Relative));
        }

        public void CreateNewExpense() {
            _navigationService.Navigate(new Uri("/CreateExpenseView.xaml", UriKind.Relative));
        }

        public void ExportTrips() {
            var bodyTemplate = "Trips\n{0}\nExpenses\n{1}";
            var task = new EmailComposeTask() {
                Subject = "My driver's log",
                To = Setting.Current.Email,
                Body = string.Format(bodyTemplate, EmailHelper.Format(Trip.All),EmailHelper.Format(Expense.All))
            };
            task.Show();
        }

        public void EditItem() {
            if (SelectedIndex == -1) return;

            _navigationService.Navigate(GetItemUri());

            SelectedIndex = -1;
            NotifyOfPropertyChange(() => SelectedIndex);

        }

        private Uri GetItemUri() {
            string uriString = string.Empty;
            if (SelectedItem is Expense) {
                uriString = "/EditExpenseView.xaml?ExpenseId={0}";
            } else if (SelectedItem is Trip) {
                uriString = "/EditView.xaml?TripId={0}";
            }
            return new Uri(string.Format(uriString, SelectedItem.Id), UriKind.Relative);
        }

        public void Settings() {
            _navigationService.Navigate(new Uri("/SettingsView.xaml", UriKind.Relative));
        }

        public void Clear() {
            var proceed = _messageBoxService.Confirm("All trips and expenses will be deleted. Make sure you've exported them.");
            if (proceed) {
                Trip.Clear();
                Trip.SaveChanges();
                Expense.Clear();
                Expense.SaveChanges();
            }
        }
    }
}