using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WindowsPhoneClassLibrary1;

namespace SilverlightApplication1 {
    public partial class MainPage : UserControl {
        public MainPage() {
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
