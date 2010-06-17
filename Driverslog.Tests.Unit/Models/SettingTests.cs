using Driverslog.Models;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Models {
    public class SettingTests {

        public SettingTests() {
            Setting.Clear();
            Setting.SaveChanges();
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
    }
}