using Driverslog.Models;
using Driverslog.ViewModels;
using Victoria.Test;

namespace Driverslog.Tests.Unit.ViewModels {
    public class ListViewModelTests {

        public ListViewModelTests() {
            Trip.All.Clear();
            Trip.SaveChanges();
        }

        [Fact]
        public void should_list_all_existing_trips() {
            
            Trip.Add(new Trip());
            Trip.SaveChanges();

            var viewModel = new ListViewModel();

            Assert.Equal(1, viewModel.TripList.Count);

        }

        
    }
}