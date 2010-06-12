using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Driverslog.Models;
using Driverslog.ViewModels;

namespace WindowsPhoneClassLibrary1 {
    
    public class Class1 {

        public string TheTest() {

            Trip.All.Add(new Trip());
            Trip.SaveChanges();

            var viewModel = new ListViewModel();
            
            Assert.True(viewModel.TripList.Count == 1);

            return string.Empty;
        }

    }
}
