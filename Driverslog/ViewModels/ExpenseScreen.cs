using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    [SurviveTombstone]
    public class ExpenseScreen : ItemScreen {

        public ExpenseScreen(IMessageBoxService messageBoxService, ITrialService trialService)
            : base(messageBoxService, trialService) {
        }

        protected override void OnInitialize() {
            base.OnInitialize();
            //load if we're comming back from resume or are empty
            if (!Expense.HasRecords) {
                Expense.Load();
            }
        }

        private string _title;
        [SurviveTombstone]
        public string Title {
            get { return _title; }
            set {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private double _amount;
        [SurviveTombstone]
        public double Amount {
            get { return _amount; }
            set {
                if (value != _amount) {
                    _amount = value;
                    NotifyOfPropertyChange(() => Amount);
                }
            }
        }
    }
}