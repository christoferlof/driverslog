using Driverslog.Models;
using Driverslog.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Driverslog.Tests {
    [TestClass]
    public class CreateTripViewModelTests {
        
        [TestMethod]
        public void save_adds_trip() {

            var viewModel = new CreateTripViewModel {Car = "rocco"};
            viewModel.SaveTripCommand.Execute(null);

            Assert.AreEqual(1,Trip.All.Count);
        }

    }
}