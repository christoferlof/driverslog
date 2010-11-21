using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace Driverslog.ViewModels {
    public class TripScreen : Screen {

        public TripScreen() {
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

        private string _from;
        public string From {
            get { return _from; }
            set {
                _from = value;
                NotifyOfPropertyChange(() => From);
            }
        }

        private string _to;
        public string To {
            get { return _to; }
            set {
                _to = value;
                NotifyOfPropertyChange(() => To);
            }
        }

        private int _odometerStart;
        public int OdometerStart {
            get { return _odometerStart; }
            set {
                _odometerStart = value;
                NotifyOfPropertyChange(() => OdometerStart);
            }
        }

        private int _odometerStop;
        public int OdometerStop {
            get { return _odometerStop; }
            set {
                _odometerStop = value;
                NotifyOfPropertyChange(() => OdometerStop);
            }
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