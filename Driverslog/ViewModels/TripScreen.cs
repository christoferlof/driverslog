using System;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;
using Microsoft.Phone.Controls;

namespace Driverslog.ViewModels {
    using System.Collections.ObjectModel;

    [SurviveTombstone]
    public class TripScreen : ItemScreen {

        public TripScreen(IMessageBoxService messageBoxService, ITrialService trialService, IAnalyticsService analyticsService) 
            : base(messageBoxService, trialService, analyticsService) {
        }

        protected override void OnInitialize() {
            base.OnInitialize();
            //load if we're comming back from resume or are empty
            if (!Trip.HasRecords) 
            {
                Trip.Load();
            }

            if (!Location.HasRecords)
            {
                Location.Load();
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

        public ObservableCollection<Location> AutoCompleteLocations
        {
            get
            {
                return Location.All;
            }
        } 

        private string _odometerStart;
        [SurviveTombstone]
        public string OdometerStart {
            get { return _odometerStart; }
            set {
                _odometerStart = value;
                NotifyOfPropertyChange(() => OdometerStart);
            }
        }

        private string _odometerStop;
        [SurviveTombstone]
        public string OdometerStop {
            get { return _odometerStop; }
            set {
                _odometerStop = value;
                NotifyOfPropertyChange(() => OdometerStop);
            }
        }

        private string _mileage;
        [SurviveTombstone]
        public string Mileage {
            get { return _mileage; }
            set {
                _mileage = value;
                NotifyOfPropertyChange(() => Mileage);
            }
        }

        public bool HideOdoFields {
            get{ return Setting.Current.HideOdoFields;}
        }

        public bool HideMileageField {
            get { return Setting.Current.HideMileageField; }
        }

        private bool _isRoundtrip;
        [SurviveTombstone]
        public bool IsRoundtrip {
            get { return _isRoundtrip; }
            set {
                _isRoundtrip = value;
                NotifyOfPropertyChange(() => IsRoundtrip);
            }
        }
    }
}