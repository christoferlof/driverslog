using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Models {
    public class LogItemTests {
        [Fact]
        public void should_set_car_from_settings() {
            
            Setting.Current.DefaultCar = "car";

            var logItem = new FakeLogItem();

            Assert.Equal(Setting.Current.DefaultCar,logItem.Car);

        }

        public class FakeLogItem : LogItem<FakeLogItem> {}
    }
}