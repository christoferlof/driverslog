using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Driverslog.Commands;

namespace Driverslog.ViewModels {
    public class CreateTripViewModel {

        public CreateTripViewModel() {
            SaveTripCommand = new DelegateCommand(SaveTrip);
        }

        private void SaveTrip(object obj) {
            MessageBox.Show("foo");
        }

        public string From { get; set; }

        public string To { get; set; }

        public int OdometerStart { get; set; }

        public int OdometerStop { get; set; }

        public int Distance { get; private set; }

        public string Notes { get; set; }

        public ICommand SaveTripCommand { get; private set; }
    }
}
