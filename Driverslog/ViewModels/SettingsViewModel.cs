using System.Diagnostics;
using System.Reflection;
using System.Windows.Controls;
using Caliburn.Micro;
using Driverslog.Models;
using Driverslog.Services;

namespace Driverslog.ViewModels {
    public class SettingsViewModel : Screen {
        private readonly INavigationService _navigationService;
        private readonly ITrialService _trialService;

        private readonly IMessageBoxService messageBoxService;

        private string _car;
        private string _email;
        private string _distanceUnit;

        public SettingsViewModel(INavigationService navigationService, ITrialService trialService, IMessageBoxService messageBoxService) {
            _navigationService = navigationService;
            _trialService = trialService;
            this.messageBoxService = messageBoxService;
        }

        public string Car {
            get {
                return _car;
            }
            set {
                _car = value;
                NotifyOfPropertyChange(() => Car);
            }
        }

        public string Email {
            get {
                return _email;
            }
            set {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }

        public string DistanceUnit {
            get {
                return _distanceUnit;
            }
            set {
                _distanceUnit = value;
                NotifyOfPropertyChange(() => DistanceUnit);
            }
        }

        protected override void OnInitialize() {
            Email            = Setting.Current.Email;
            DistanceUnit     = Setting.Current.DistanceUnit;
            Car              = Setting.Current.DefaultCar;
            HideOdoFields    = Setting.Current.HideOdoFields;
            HideMileageField = Setting.Current.HideMileageField;
        }

        public void SaveSettings() {
            Setting.Current.Email            = Email;
            Setting.Current.DistanceUnit     = DistanceUnit;
            Setting.Current.DefaultCar       = Car;
            Setting.Current.HideMileageField = HideMileageField;
            Setting.Current.HideOdoFields    = HideOdoFields;
            Setting.SaveChanges();

            _navigationService.GoBack();
        }

        public string ApplicationVersion {
            get {
                return string.Format("Version {0}{1}",
                    GetType().Assembly.FullName.Split('=')[1].Split(',')[0],
                    (_trialService.IsTrial) ? " Trial" : "");
            }
        }

        public bool HideOdoFields { get; set; }

        public void OnHideOdoFieldsChanged(SelectionChangedEventArgs args) {
            HideOdoFields = SelectionChangedToBool(args);
        }

        public bool HideMileageField { get; set; }

        public void OnHideMileageFieldChanged(SelectionChangedEventArgs args) {
            HideMileageField = SelectionChangedToBool(args);
        }

        public void ClearSuggestions()
        {
            if (!this.messageBoxService.Confirm(Strings.ClearSuggestions, Strings.ClearSuggestionsConfirm))
            {
                return;
            }

            Location.Clear();
            Location.SaveChanges();
        }

        private static bool SelectionChangedToBool(SelectionChangedEventArgs args) {
            return ((ContentControl)args.AddedItems[0]).Content.ToString().ToLower().Equals(Strings.Yes.ToLower());
        }
    }
}