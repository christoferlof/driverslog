using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Victoria.Test.Exceptions;
using Victoria.Test.Tests.Unit.Runner;

namespace Victoria.Test.Runner {
    public class TestRunner {
        private readonly TestMethodResolver _testMethodResolver;

        private int _passedCounter;
        private int _failedCounter;

        /// <summary>
        /// Creates the testrunner with default TestMethodResolver
        /// </summary>
        public TestRunner()
            : this(new TestMethodResolver(new TestAssemblyResolver())) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testMethodResolver"></param>
        public TestRunner(TestMethodResolver testMethodResolver) {
            _testMethodResolver = testMethodResolver;
        }

        /// <summary>
        /// Executes the suite of tests
        /// </summary>
        /// <param name="testPath">The path of the test(s) to execute.</param>
        /// <returns>True if the testrun succeeded; otherwise false.</returns>
        /// <remarks>
        /// Supported test paths are
        ///   - Full path to single testmethod. Will execute single testmethod. name.space.Tests.Unit.testmethod
        ///   - Root namespace. Will execute all tests within that project. 
        ///     By convention the project namespace must end with "Tests.Unit". name.space.Tests.Unit
        ///   - Empty. Will execute all testmethods found in the assemblies that are returned by the TestAssemblyResolver.
        /// </remarks>
        public bool Execute(string testPath) {
            try {

                _testMethodResolver.LoadTestAssemblies();

                var methods = _testMethodResolver.GetTestMethods(testPath);
                if (!methods.Any()) return ExitRun(false, "Couldn't find any matching test methods");

                var testrunPass = true;
                Console.WriteLine(string.Empty); //new line

                foreach (var method in methods) {

                    var testClass = Activator.CreateInstance(method.DeclaringType);

                    var testmethodPass = false;
                    var failedMessage = string.Empty;
                    try {
                        //invoke testmethod
                        testClass.GetType().InvokeMember(
                            method.Name,
                            BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                            null,
                            testClass,
                            null
                            );
                        testmethodPass = true;
                        _passedCounter++;
                    } catch (Exception ex) {
                        testmethodPass = false;
                        testrunPass = false;
                        _failedCounter++;
                        if (ex.InnerException is AssertException) {
                            failedMessage = string.Format("=> {0}", ex.InnerException.Message);
                        } else {
                            failedMessage = string.Format("=> {0}: {1}", ex.InnerException.GetType().Name,
                                                          ex.InnerException.Message);
                        }
                    }

                    var restultMessage = string.Format("{0} {1}.{2} {3}",
                                                       (testmethodPass) ? "Passed" : "Failed",
                                                       method.DeclaringType.Name,
                                                       method.Name,
                                                       failedMessage);
                    Console.WriteLine(restultMessage);
                }

                return ExitRun(testrunPass, string.Empty);
            }
            catch(Exception ex) {
                Console.WriteLine(string.Empty); //new line
                Console.WriteLine("Catastrophic failure!");
                Console.WriteLine(ex.ToString());
                return false;
            }

        }

        private bool ExitRun(bool testrunPass, string message) {
            var testrunMessage = string.Format("\nTestrun {0}. {1}", (testrunPass) ? "succeeded" : "failed", message);
            Console.WriteLine(testrunMessage);
            Console.WriteLine(string.Format("{0} tests passed",_passedCounter));
            Console.WriteLine(string.Format("{0} tests failed", _failedCounter));
            return testrunPass;
        }
    }
}