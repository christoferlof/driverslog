using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Caliburn.Micro;
using Driverslog.Commands;
using Driverslog.Helpers;
using Driverslog.Models;
using Microsoft.Phone.Tasks;
using Victoria.Data;

namespace Driverslog.ViewModels {
    public class MainPageViewModel : Screen {
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
            SelectedIndex = -1;
            
            //todo: async
            Trip.Load();
            Expense.Load();
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
            //MessageBox.Show(EmailHelper.Format(Trip.ByDateDescending()));
            var task = new EmailComposeTask() {
                Subject = "Your drive log",
                Body = EmailHelper.Format(Trip.All)
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
            return new Uri(string.Format(uriString,SelectedItem.Id),UriKind.Relative);
        }
    }
}