using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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

            var actualMethods = new List<string>();
            
            if(!string.IsNullOrEmpty(testMethod)) {
                actualMethods.Add(testMethod);
            }

            if(string.IsNullOrEmpty(testMethod)) {
                actualMethods.AddRange(
                    testClass
                        .GetType()
                        .FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance, null, null)
                        .Select(m => m.Name)
                        .Where(s => s.Contains("Test"))
                    );
            }
            
            var testrunPass = true;
            
            foreach (var method in actualMethods) {

                //check if method exists
                var exists = testClass
                    .GetType()
                    .FindMembers(
                        MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance,
                        (m, f) => m.Name == f.ToString(), method)
                    .Any();
                if (!exists) testrunPass = false;

                //if method exists => ok to execute method
                if (exists) {
                    var testmethodPass = false;
                    var failedMessage = string.Empty;
                    try {
                        //invoke testmethod
                        testClass.GetType().InvokeMember(
                            method,
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                            null,
                            testClass,
                            null
                            );
                        testmethodPass = true;
                    }
                    catch (Exception ex) {
                        testmethodPass = false;
                        testrunPass = false;
                        failedMessage = string.Format("=> {0}", ex.InnerException.Message);
                    }

                    var testClassTypeForMessage = testClassType.Split('.').Last();
                    var restultMessage = string.Format("{0} {1}.{2} {3}",
                                                       (testmethodPass) ? "Passed" : "Failed",
                                                       testClassTypeForMessage,
                                                       method,
                                                       failedMessage);
                    Console.WriteLine(restultMessage);
                }
            }
            var testrunMessage = string.Format("\nTestrun {0}", (testrunPass) ? "succeeded" : "failed");
            Console.WriteLine(testrunMessage);

            return (testrunPass) ? 0 : 1;
        }
    }
}
