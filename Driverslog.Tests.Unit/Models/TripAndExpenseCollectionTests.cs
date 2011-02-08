using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Models {
    public class TripAndExpenseCollectionTests {
        
        [Fact]
        public void should_add_trip() {
            
            var collection = new TripAndExpenseCollection();

            Trip.Add(new Trip());

            Assert.Equal(1,collection.Count);

        }

        [Fact]
        public void should_add_expense() {
            
            var collection = new TripAndExpenseCollection();

            Expense.Add(new Expense());

            Assert.Equal(1,collection.Count);
        }

        [Fact]
        public void should_remove_trip() {
           
            var collection = new TripAndExpenseCollection();

            var trip = new Trip();
            Trip.Add(trip);
            Trip.All.Remove(trip);

            Assert.Equal(0,collection.Count);
        }

        [Fact]
        public void should_remove_expense() {

            var collection = new TripAndExpenseCollection();

            var expense = new Expense();
            Expense.Add(expense);
            Expense.All.Remove(expense);

            Assert.Equal(0, collection.Count);
        }

        [Fact]
        public void should_update_trip() {
            
            var collection = new TripAndExpenseCollection();

            var trip = new Trip{From = "from"};
            Trip.Add(trip);
            trip.From = "updated";

            Assert.Equal(trip.From,((Trip)collection[0]).From);
        }

        [Fact]
        public void should_update_expense() {
            
            var collection = new TripAndExpenseCollection();

            var expense = new Expense{Title = "title"};
            Expense.Add(expense);
            expense.Title = "updated";

            Assert.Equal(expense.Title,((Expense)collection[0]).Title);
        }

        [Fact]
        public void should_clear() {
            var collection = new TripAndExpenseCollection();

            Expense.Add(new Expense());
            Trip.Add(new Trip());

            Assert.Equal(2,collection.Count);

            Expense.Clear();
            Assert.Equal(1,collection.Count);

            Trip.Clear();
            Assert.Equal(0,collection.Count);
        }
    }
}