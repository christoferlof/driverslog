using System;
using System.Linq;
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
        public int ExecuteTest(string testMethod) {

            var testAssembly = Assembly.Load("Driverslog.Tests.Unit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            var testClassType = "Driverslog.Tests.Unit.ListViewModelTests";
            var testClass = testAssembly.CreateInstance(testClassType);
            
            var pass = false;
            var failedMessage = string.Empty;
            try {
                testClass.GetType().InvokeMember(
                    testMethod,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                    null,
                    testClass,
                    null
                );
                //var c = new ListViewModelTests();
                //c.TheTest();
                pass = true;
            //} catch (AssertException ae) {
            //    //assertexception
            //    pass = false;
            //    failedMessage = ae.Message;
            } catch (Exception ex) {
                pass = false;
                failedMessage = string.Format("=> {0}", ex.InnerException.Message);
            }

            var testClassTypeForMessage = testClassType.Split('.').Last();
            var restultMessage = string.Format("{0} {1}.{2} {3}",
                                               (pass) ? "Passed" : "Failed",
                                               testClassTypeForMessage,
                                               testMethod,
                                               failedMessage);
            Console.WriteLine(restultMessage);
            return (pass) ? 0 : 1;
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
