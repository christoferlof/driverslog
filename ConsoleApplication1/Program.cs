using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;

namespace ConsoleApplication1 {
    class Program {
        [STAThread]
        static void Main(string[] args) {

            var browser = new WebBrowser();
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            var url = Path.GetDirectoryName(typeof(Program).Assembly.CodeBase) +
                       "/SilverlightRunner/SilverlightApplication1TestPage.html";
            browser.Navigate(url);
            Application.Run();
        }

        static void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            Application.DoEvents();
            var browser = (WebBrowser) sender;
            var testResult = browser.Document.InvokeScript("executeTest");
            Console.WriteLine(testResult);
            Application.Exit();
        }
    }
}
