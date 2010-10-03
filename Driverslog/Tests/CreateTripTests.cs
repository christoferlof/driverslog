using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Controls;
using System.Collections.Generic;
using Microsoft.Phone.Shell;
using Victoria.Test;

namespace Driverslog.Tests {
    public class CreateTripTests : IntegrationTest {

        [Fact]
        public void should_hold_to_and_car() {
            Page("/views/CreateTrip.xaml").Ready(page => {
                
                page.Find<TextBox>("txtto").SetText("foo");
                page.Find<TextBox>("txtcar").SetText("bar");

                //page.WaitForNavigation(() =>
                    page.ApplicationBar.Button("Check").Click();
                //);
                
                //Debug.WriteLine(page.Find<TextBox>("txtto").GetText());
            });
        }

        [Fact]
        public void should_list_trips() {
            Debug.WriteLine("list");
            Page("/views/List.xaml").Ready(page => {

                Debug.WriteLine("List ready");
            });
        }
    }
}