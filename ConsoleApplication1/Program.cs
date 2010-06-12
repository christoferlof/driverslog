using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using SilverlightClassLibrary1;


namespace ConsoleApplication1 {
    class Program {
        static void Main(string[] args) {

            //var asm = @"C:\Code\Driverslog\WindowsPhoneClassLibrary1\Bin\Debug\WindowsPhoneClassLibrary1.dll";
            //var testAssembly = Assembly.LoadFrom(asm);

            
            //var testClassType = "WindowsPhoneClassLibrary1.Class1";
            ////var testClass = domain.CreateInstance(testAssembly.FullName, testClassType);
            //var testClass = testAssembly.CreateInstance(testClassType);
            //var result = testClass.GetType().InvokeMember("TheTest", BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod, null, testClass,
            //                                 null);
            
            //Console.WriteLine(result);
            //Console.Read();

            var c = new Class1();
            Console.WriteLine(c.TheTest());
            Console.Read();

        }
    }
}
