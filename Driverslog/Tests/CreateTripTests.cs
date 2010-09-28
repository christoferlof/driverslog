using System.Diagnostics;
using System.Windows.Controls;
using Victoria.Test;
using Victoria.Test.UI;

namespace Driverslog.Tests {
    public class CreateTripTests : IntegrationTest {

        public void should_hold_to_and_car() {
            new AutomationPage("/views/CreateTrip.xaml", RootFrame).Ready(page => {
                page.Find<TextBox>("txtto").SetText("foo");
                page.Find<TextBox>("txtcar").SetText("bar");

                Debug.WriteLine(page.Find<TextBox>("txtto").GetText());
            });
        }
    }
}