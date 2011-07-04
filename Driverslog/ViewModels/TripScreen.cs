using System;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;
using Microsoft.Phone.Controls;

namespace Driverslog.ViewModels {
    [SurviveTombstone]
    public class TripScreen : ItemScreen {

        public TripScreen(IMessageBoxService messageBoxService, ITrialService trialService) 
            : base(messageBoxService, trialService) {
        }

        protected override void OnInitialize() {
            base.OnInitialize();
            //load if we're comming back from resume or are empty
            if (!Trip.HasRecords) {
                Trip.Load();
            }
        }

        private string _from;
        [SurviveTombstone]
        public string From {
            get { return _from; }
            set {
                _from = value;
                NotifyOfPropertyChange(() => From);
            }
        }

        private string _to;
        [SurviveTombstone]
        public string To {
            get { return _to; }
            set {
                _to = value;
                NotifyOfPropertyChange(() => To);
            }
        }

        private int _odometerStart;
        [SurviveTombstone]
        public int OdometerStart {
            get { return _odometerStart; }
            set {
                _odometerStart = value;
                NotifyOfPropertyChange(() => OdometerStart);
            }
        }

        private int _odometerStop;
        [SurviveTombstone]
        public int OdometerStop {
            get { return _odometerStop; }
            set {
                _odometerStop = value;
                NotifyOfPropertyChange(() => OdometerStop);
            }
        }

        private int _mileage;
        [SurviveTombstone]
        public int Mileage {
            get { return _mileage; }
            set {
                _mileage = value;
                NotifyOfPropertyChange(() => Mileage);
            }
        }

    }
}