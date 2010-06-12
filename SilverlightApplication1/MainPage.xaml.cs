using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WindowsPhoneClassLibrary1;

//using WindowsPhoneClassLibrary1;

namespace SilverlightApplication1 {
    public partial class MainPage : UserControl {
        public MainPage() {
            InitializeComponent();


            //var testAssembly = Assembly.Load("WindowsPhoneClassLibrary1");
            //var testClassType = "WindowsPhoneClassLibrary1.Class1";
            //var testClass = testAssembly.CreateInstance(testClassType);
            //var result = testClass.GetType().InvokeMember("TheTest", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, testClass,
            //                                 null);

            //textBox1.Text = result.ToString();

            ////Console.WriteLine(result);
            ////Console.Read();

            var c = new Class1();
            var pass = false;
            try {
                c.TheTest();
                pass = true;
            }
            catch(ArgumentException) {
                //assertexception
                pass = false;
            }
            catch(Exception) {
                pass = false;
            }

            textBox1.Text = (pass) ? "passed" : "failed";
        }
    }
}
