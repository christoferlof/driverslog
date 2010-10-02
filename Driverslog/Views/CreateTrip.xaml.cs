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

namespace Driverslog.Views
{
    public partial class CreateTrip : PhoneApplicationPage
    {
        public CreateTrip()
        {
            InitializeComponent();
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            //if(e.Key == Key.Enter) {
            //    e.Handled = true;
            //    btn.Focus();
            //}
        }

        public void Check_Click(object sender, EventArgs e) {
            ((CreateTripViewModel)DataContext).SaveTripCommand.Execute(null);
        }
    }
}