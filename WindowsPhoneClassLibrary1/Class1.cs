﻿using Driverslog.Models;
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
