using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO.IsolatedStorage;
using Caliburn.Micro;
using Driverslog.Commands;
using Driverslog.Models;

namespace Driverslog.ViewModels {
    public class CreateViewModel : Screen {
        private readonly INavigationService _navigationService;

        public CreateViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
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

        public int Distance { get; private set; }

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

        public void SaveTrip() {
            Trip.All.Add(new Trip {
                Car             = Car,
                From            = From,
                Notes           = Notes,
                OdometerStart   = OdometerStart,
                OdometerStop    = OdometerStop,
                To              = To
            });     
            Trip.SaveChanges();
            _navigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}
