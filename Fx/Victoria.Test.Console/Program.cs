using System;
using System.IO;
using System.Windows.Forms;

namespace Victoria.Test.Console {
    class Program {
        [STAThread]
        static void Main(string[] args) {

            var browser = new WebBrowser();
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            var url = Path.GetDirectoryName(typeof(Program).Assembly.CodeBase) +
                       "/Content/RunnerPage.html";
            browser.Navigate(url);
            Application.Run();
        }

        static void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) {
            Application.DoEvents();
            var browser = (WebBrowser) sender;
            var testResult = browser.Document.InvokeScript("executeTest");
            System.Console.WriteLine(testResult);
            Application.Exit();
        }
    }
}
