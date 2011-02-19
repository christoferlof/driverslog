using System.Reflection;
using Caliburn.Micro;
using Driverslog.Models;

namespace Driverslog.ViewModels {
    public class SettingsViewModel : Screen {
        private readonly INavigationService _navigationService;
        private string _car;
        private string _email;
        private string _distanceUnit;

        public SettingsViewModel(INavigationService navigationService) {
            _navigationService = navigationService;
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
            Email = Setting.Current.Email;
            DistanceUnit = Setting.Current.DistanceUnit;
            Car = Setting.Current.DefaultCar;
        }

        public void SaveSettings() {
            Setting.Current.Email = Email;
            Setting.Current.DistanceUnit = DistanceUnit;
            Setting.Current.DefaultCar = Car;
            Setting.SaveChanges();

            _navigationService.GoBack();
        }

        public string ApplicationVersion {
            get { return "Version " + GetType().Assembly.FullName.Split('=')[1].Split(',')[0]; }
        }
    }
}