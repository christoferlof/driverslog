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
        private readonly ITrialService _trialService;

        public ItemScreen(IMessageBoxService messageBoxService, ITrialService trialService) {
            _messageBoxService = messageBoxService;
            _trialService = trialService;
            _date = DateTime.Today;
            _car = Setting.Current.DefaultCar;
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

        protected bool IsTrialLimitReached() {
            if(_trialService == null) return false;

            bool reached = _trialService.LimitReached();
            if (reached) {
                bool buy = MessageBoxService.Confirm("Trial limit reached", string.Format(
                    "You've reached the trial limit.\nYour log can contain {0} items in this trial version.\n" +
                    "Select OK in order to buy the full version of Driver's log.",
                    _trialService.Limit));
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