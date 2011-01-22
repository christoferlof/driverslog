using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace Driverslog.ViewModels {
    public class ItemScreen : Screen {
        
        public ItemScreen() {
            _date = DateTime.Today;
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