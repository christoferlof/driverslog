﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using Victoria.Test.Tests.Unit.Runner;

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
            return (new TestRunner(
                new TestMethodResolver(new TestAssemblyResolver()),
                new ConsoleOutputWriter())
                .Execute(testMethod)) ? 0 : 1;
        }

        
    }
}
