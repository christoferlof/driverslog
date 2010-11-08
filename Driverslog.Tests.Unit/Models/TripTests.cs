using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Models {
    public class TripTests {

        [Fact]
        public void should_return_informative_text_on_distance_without_stop() {
            var trip = new Trip { OdometerStart = 1 };
            Assert.Equal("Unknown distance", trip.Distance);
        }

        [Fact]
        public void should_return_informative_text_on_distance_without_start() {
            var trip = new Trip { OdometerStop = 1 };
            Assert.Equal("Unknown distance", trip.Distance);
        }

        [Fact]
        public void should_calculate_distance() {
            var trip = new Trip {
                OdometerStart = 1,
                OdometerStop = 2
            };
            Assert.Equal("1", trip.Distance);
        }
    }
}