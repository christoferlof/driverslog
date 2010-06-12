using System;
using System.Reflection;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using Driverslog.Tests.Unit;
using Victoria.Test;

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

            //var testAssembly = Assembly.Load("Driverslog.Tests.Unit");
            //var testClassType = "Driverslog.Tests.Unit.ListViewModelTests";
            //var testClass = testAssembly.CreateInstance(testClassType);
            
            var pass = false;
            try {
                //testClass.GetType().InvokeMember(
                //    "TheTest", 
                //    BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, 
                //    null, 
                //    testClass,
                //    null
                //);
                var c = new ListViewModelTests();
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

        //[DataContract(Name = "executionResult")]
        //public class ExecutionResult {
        //    [DataMember(Name = "passed")]
        //    public bool Passed { get; set; }
        //}

        

        //textBox1.Text = result.ToString();

        ////Console.WriteLine(result);
        ////Console.Read();
    }
}
