using System;
using Driverslog.Helpers;
using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Helpers {
    public class when_formatting_expenses_for_email_spec : ContextSpecification {

        protected string Result;

        public override void Context() {
            Expense.Clear();
            Expense.Add(new Expense{ Amount = 1, Car = "car", Date = DateTime.Now.Date, Notes = "notes", Title = "title"});
            Expense.Add(new Expense { Amount = 2, Car = "car2", Date = DateTime.Now.Date.AddDays(1), Notes = "notes2", Title = "title2" });
        }

        public override void Because() {
            Result = EmailHelper.Format(Expense.All);
        }

        [Fact]
        public void should_contain_one_trip_per_row() {
            var rows = Result.Split('\n');
            Assert.Equal(2,rows.Length-2); //subtract header and last row (empty)
        }

        [Fact]
        public void should_include_headers_on_first_row() {
            var header = Result.Split('\n')[0];

            Assert.True(header.Contains("Date"));
            Assert.True(header.Contains("Car"));
            Assert.True(header.Contains("Notes"));
            Assert.True(header.Contains("Amount"));
            Assert.True(header.Contains("Title"));
            
        }

        [Fact]
        public void should_delimit_fields_with_semicolon() {
            var firstTrip = Result.Split('\n')[1];
            var cols = firstTrip.Split(',');

            Assert.Equal(6, cols.Length);

        }

    }
}