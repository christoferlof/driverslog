using System;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit {
    public class ListViewModelTests {
        
        public string TheTest() {

            Trip.All.Add(new Trip());
            Trip.SaveChanges();

            var viewModel = new ListViewModel();

            Assert.True(viewModel.TripList.Count == 1);

            return string.Empty;
        }
    }
}