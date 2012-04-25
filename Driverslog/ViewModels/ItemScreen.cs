using System;
using System.Linq;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;
using Microsoft.Phone.Controls;

namespace Driverslog.ViewModels {
    [SurviveTombstone]
    public class ItemScreen : Screen {
        private readonly IMessageBoxService _messageBoxService;
        private readonly ITrialService      _trialService;
        private readonly IAnalyticsService  _analyticsService;

        public ItemScreen(IMessageBoxService messageBoxService, ITrialService trialService, IAnalyticsService analyticsService) {
            _messageBoxService  = messageBoxService;
            _trialService       = trialService;
            _analyticsService   = analyticsService;
            _date               = DateTime.Today;
            _car                = Setting.Current.DefaultCar;
        }

        protected IMessageBoxService MessageBoxService {
            get { return _messageBoxService; }
        }

        protected IAnalyticsService AnalyticsService {
            get { return _analyticsService; }
        }

        protected bool IsValid(ICanHaveValidationErrors iCanHaveValidationErrors) {
            if (!iCanHaveValidationErrors.IsValid()) {
                MessageBoxService.ShowMessage(
                    string.Join("\n", iCanHaveValidationErrors.ValidationMessages.Select(x => x.Value).ToArray()));
                return false;
            }
            return true;
        }

        protected bool IsTrialLimitReached() {
            if(_trialService == null) return false;

            bool reached = _trialService.LimitReached();
            if (reached) {
                bool buy = MessageBoxService.Confirm(Strings.TrialLimitReachedTitle, 
                    string.Format(Strings.TrialLimitReachedMessage, _trialService.Limit));
                if (buy) {
                    _trialService.Buy();
                }
            }
            return reached;
        }

        private DateTime _date;
        [SurviveTombstone]
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
        [SurviveTombstone]
        public string Car {
            get { return _car; }
            set {
                _car = value;
                NotifyOfPropertyChange(() => Car);
            }
        }

        private string _notes;
        [SurviveTombstone]
        public string Notes {
            get { return _notes; }
            set {
                _notes = value;
                NotifyOfPropertyChange(() => Notes);
            }
        }

    }
}