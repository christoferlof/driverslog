namespace Driverslog.ViewModels {
    public class ExpenseScreen : ItemScreen {
        private string _title;
        public string Title {
            get { return _title; }
            set {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        private double _amount;
        public double Amount {
            get { return _amount; }
            set {
                _amount = value;
                NotifyOfPropertyChange(() => Amount);
            }
        }
    }
}