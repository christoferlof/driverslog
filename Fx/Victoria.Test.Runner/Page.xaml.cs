using System;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace Victoria.Test.Runner {
    public partial class Page : UserControl {
        public Page() {
            InitializeComponent();
        }

        public void MainPage_Loaded(object sender, RoutedEventArgs e) {
            HtmlPage.RegisterScriptableObject("runner",this);
        }

        [ScriptableMember]
        public string ExecuteTest() {
            var c = new Class1();
            var pass = false;
            try {
                c.TheTest();
                pass = true;
            } catch (AssertException) {
                //assertexception
                pass = false;
            } catch (Exception) {
                pass = false;
            }

            return (pass) ? "passed" : "failed";
        }

        //var testAssembly = Assembly.Load("WindowsPhoneClassLibrary1");
        //var testClassType = "WindowsPhoneClassLibrary1.Class1";
        //var testClass = testAssembly.CreateInstance(testClassType);
        //var result = testClass.GetType().InvokeMember("TheTest", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, testClass,
        //                                 null);

        //textBox1.Text = result.ToString();

        ////Console.WriteLine(result);
        ////Console.Read();
    }
}
