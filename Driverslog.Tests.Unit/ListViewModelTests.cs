using System;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit {
    public class ListViewModelTests {

        public void TheTest() {

            Trip.All.Add(new Trip());
            Trip.SaveChanges();

            var viewModel = new ListViewModel();

            Assert.Equal(1,viewModel.TripList.Count);
        }

        public void TheTestAgain() {

            var viewModel = new ListViewModel();

            Assert.Equal(1, viewModel.TripList.Count);

        }

        public void FailingTest() {
            Assert.True(false);
        }

        public void ExplodingTest() {
            throw new InvalidOperationException("eek");
        }
    }
}