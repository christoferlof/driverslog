using System;
using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit {
    public class ListViewModelTests {

        public ListViewModelTests() {
            Trip.All.Clear();
            Trip.SaveChanges();
        }

        [Fact]
        public void TheTest() {

            Trip.All.Add(new Trip());
            Trip.SaveChanges();

            var viewModel = new ListViewModel();

            Assert.Equal(1,viewModel.TripList.Count);
        }

        [Fact]
        public void TheTestAgain() {

            var viewModel = new ListViewModel();

            Assert.Equal(0, viewModel.TripList.Count);

        }

        [Fact]
        public void FailingTest() {
            Assert.True(false);
        }

        [Fact]
        public void ExplodingTest() {
            throw new InvalidOperationException("eek");
        }
    }
}