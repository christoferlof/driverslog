using System;
using Driverslog.Helpers;
using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Helpers {
    public class when_formatting_email_spec : ContextSpecification {

        protected string Result;

        public override void Context() {
            Trip.Clear();
            Trip.Add(new Trip { Date = DateTime.Now, Car = "car1", From = "from1", To = "to1", OdometerStart = 1, OdometerStop = 2, Notes = "notes1" });
            Trip.Add(new Trip { Date = DateTime.Now.AddDays(-1), Car = "car2", From = "from2", To = "to2", OdometerStart = 3, OdometerStop = 4, Notes = "notes2" });
        }

        public override void Because() {
            Result = EmailHelper.Format(Trip.All);
        }

        [Fact]
        public void should_contain_one_trip_per_row() {
            var rows = Result.Split('\n');
            Assert.Equal(2,rows.Length-2); //subtract header and last row (empty)
        }

        [Fact]
        public void should_include_headers_on_first_row() {
            var header = Result.Split('\n')[0];

            Assert.True(header.Contains("Car"));
            Assert.True(header.Contains("To"));
            Assert.True(header.Contains("From"));
            Assert.True(header.Contains("Notes"));
            Assert.True(header.Contains("Start"));
            Assert.True(header.Contains("Stop"));
            Assert.True(header.Contains("Distance"));
            Assert.True(header.Contains("Date"));
        }

        [Fact]
        public void should_delimit_fields_with_semicolon() {
            var firstTrip = Result.Split('\n')[1];
            var cols = firstTrip.Split(',');

            Assert.Equal(8, cols.Length);

        }

    }
}