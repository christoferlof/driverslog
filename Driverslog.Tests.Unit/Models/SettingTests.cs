using System.Threading;
using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Models {
    public class SettingTests {

        public SettingTests() {
            Setting.Clear();
            Setting.SaveChanges();
            Setting.Kill();
        }

        [Fact]
        public void should_return_single_setting() {
            Assert.True(Setting.Current is Setting);
        }

        [Fact]
        public void should_load_saved_settings() {
            var email = "foo@bar.com";

            Setting.Current.Email = email;
            Setting.SaveChanges();

            Setting.Clear(); //"unload"

            Assert.Equal(email, Setting.Current.Email);

        }

        [Fact]
        public void should_set_miles_as_default_unit_for_us() {
            var tmp = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            Assert.Equal("miles",Setting.Current.DistanceUnit);

            Thread.CurrentThread.CurrentCulture = tmp; //reset
        }

        [Fact]
        public void should_set_miles_as_default_unit_for_gb() {
            var tmp = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");

            Assert.Equal("miles", Setting.Current.DistanceUnit);

            Thread.CurrentThread.CurrentCulture = tmp; //reset
        }

        [Fact]
        public void should_set_km_as_default_unit_for_se() {
            var tmp = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("sv-SE");

            Assert.Equal("km", Setting.Current.DistanceUnit);

            Thread.CurrentThread.CurrentCulture = tmp; //reset
        }
    }
}