using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using Caliburn.Micro;
using Driverslog.Helpers;
using Driverslog.Models;
using Driverslog.Services;
using Microsoft.Phone.Tasks;

namespace Driverslog.ViewModels {
    [SurviveTombstone]
    public class MainPageViewModel : Screen {
        private readonly INavigationService _navigationService;
        private readonly IMessageBoxService _messageBoxService;
        private readonly IAnalyticsService _analyticsService;

        public MainPageViewModel(INavigationService navigationService, IMessageBoxService messageBoxService, IAnalyticsService analyticsService) {
            _navigationService = navigationService;
            _messageBoxService = messageBoxService;
            _analyticsService = analyticsService;
            SelectedIndex = -1;
        }

        protected override void OnInitialize() {
            Deployment.Current.Dispatcher.BeginInvoke(() => {
                Trip.Load();
                Expense.Load();
            });
          
        }

        protected override void OnViewLoaded(object view) {
            base.OnViewLoaded(view);
            NotifyOfPropertyChange(() => SelectedPivotIndex);
        }

        public ObservableCollection<Trip> TripList {
            get { return Trip.All; }
        }

        public ObservableCollection<Expense> ExpenseList {
            get { return Expense.All; }
        }

        public IHaveId SelectedItem { get; set; }

        public int SelectedIndex { get; set; }

        [SurviveTombstone]
        public int SelectedPivotIndex { get; set; }

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
                Body = string.Format(bodyTemplate, EmailHelper.Format(Trip.All), EmailHelper.Format(Expense.All))
            };
            LogCountEvent("Export");
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
            var proceed = _messageBoxService.Confirm(Strings.MainPageClearConfirm);
            if (proceed) {
                LogCountEvent("Clear");
                Trip.Clear();
                Trip.SaveChanges();
                Expense.Clear();
                Expense.SaveChanges();
            }
        }

        private void LogCountEvent(string eventName) {
            _analyticsService.LogEvent(eventName,
                new Dictionary<string, string> { { "Item.Count", (Trip.All.Count + Expense.All.Count).ToString() } });
        }
    }
}