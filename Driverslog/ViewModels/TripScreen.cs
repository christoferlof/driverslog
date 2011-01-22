using System;
using Caliburn.Micro;
using Microsoft.Phone.Controls;

namespace Driverslog.ViewModels {
    public class TripScreen : ItemScreen {

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

    }
}