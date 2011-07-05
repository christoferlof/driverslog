using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Driverslog.ViewModels;
using Microsoft.Phone.Controls;

namespace Driverslog {
    public partial class EditView : PhoneApplicationPage {
        public EditView() {
            InitializeComponent();
        }

        private void ToggleSwitch_Checked(object sender, RoutedEventArgs e) {
            ((EditViewModel)DataContext).IsRoundtrip = true;
        }

        private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e) {
            ((EditViewModel)DataContext).IsRoundtrip = false;
        }
    }
}