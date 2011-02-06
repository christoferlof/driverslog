using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;
using Microsoft.Phone.Controls;

namespace Driverslog.ViewModels {
    public class ItemScreen : Screen {
        private readonly IMessageBoxService _messageBoxService;

        public ItemScreen(IMessageBoxService messageBoxService) {
            _messageBoxService = messageBoxService;
            _date = DateTime.Today;
        }

        protected IMessageBoxService MessageBoxService {
            get{return _messageBoxService;}
        }

        protected bool IsValid(ICanHaveValidationErrors iCanHaveValidationErrors) {
            if (!iCanHaveValidationErrors.IsValid()) {
                MessageBoxService.ShowMessage(
                    string.Join("\n", iCanHaveValidationErrors.ValidationMessages.Select(x => x.Value).ToArray()));
                return false;
            }
            return true;
        }

        private DateTime _date;
        public DateTime Date {
            get { return _date; }
            set {
                _date = value;
                NotifyOfPropertyChange(() => Date);
            }
        }

        public void ChangeDate(DateTimeValueChangedEventArgs e) {
            if (e.NewDateTime != null) Date = (DateTime)e.NewDateTime;
        }

        private string _car;
        public string Car {
            get { return _car; }
            set {
                _car = value;
                NotifyOfPropertyChange(() => Car);
            }
        }

        private string _notes;
        public string Notes {
            get { return _notes; }
            set {
                _notes = value;
                NotifyOfPropertyChange(() => Notes);
            }
        }

    }
}