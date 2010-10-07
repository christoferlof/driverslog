using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Victoria.Test.Exceptions;
using Victoria.Test.Tests.Unit.Runner;

namespace Victoria.Test.Runner {
    public class TestRunner {

        private readonly TestMethodResolver _testMethodResolver;
        private readonly OutputWriter       _outputWriter;

        private int _passedCounter;
        private int _failedCounter;

        /// <summary>
        /// Creates the testrunner with default TestMethodResolver and a ConsoleOutputWriter
        /// </summary>
        public TestRunner() : this( 
                new TestMethodResolver(new TestAssemblyResolver()),
                new ConsoleOutputWriter()) {}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testMethodResolver"></param>
        /// <param name="outputWriter"></param>
        public TestRunner(TestMethodResolver testMethodResolver, OutputWriter outputWriter) {
            _testMethodResolver = testMethodResolver;
            _outputWriter = outputWriter;
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

                _outputWriter.Write(string.Empty); //new line

                var testrunPass = true;
                foreach (var method in methods) {

                    var methodResult = ExecuteMethod(method);
                    if(!methodResult) {
                        testrunPass = false;
                        _failedCounter ++;
                    }
                    if (methodResult) {
                        _passedCounter++;
                    }

                }

                return ExitRun(testrunPass, string.Empty);
            }
            catch(Exception ex) {
                _outputWriter.Write(string.Empty); //new line
                _outputWriter.Write("Catastrophic failure!");
                _outputWriter.Write(ex.ToString());
                return false;
            }

        }

        private bool ExecuteMethod(MemberInfo method) {

            var testObject = Activator.CreateInstance(method.DeclaringType);

            var testmethodPass = false;
            var failedMessage = string.Empty;
            try {
                InvokeTestMethod(method, testObject);
                testmethodPass = true;
            }
            catch (Exception ex) {
                failedMessage = HandleTestMethodException(ex);
                testmethodPass = false;
            }

            var restultMessage = FormatRestultMessage(method, testmethodPass, failedMessage);
            _outputWriter.Write(restultMessage);
            return testmethodPass;
        }

        private void InvokeTestMethod(MemberInfo method, object testClass) {
            testClass.GetType().InvokeMember(
                method.Name,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
                null,
                testClass,
                null
                );
        }

        private string FormatRestultMessage(MemberInfo method, bool testmethodPass, string failedMessage) {
            return string.Format("{0} {1}.{2} {3}",
                                 (testmethodPass) ? "Passed" : "Failed",
                                 method.DeclaringType.Name,
                                 method.Name,
                                 failedMessage);
        }

        private string HandleTestMethodException(Exception ex) {
            string failedMessage;
            if (ex.InnerException is AssertException) {
                failedMessage = string.Format("=> {0}", ex.InnerException.Message);
            } else {
                failedMessage = string.Format("=> {0}: {1}", ex.InnerException.GetType().Name,
                                              ex.InnerException.Message);
            }
            return failedMessage;
        }

        private bool ExitRun(bool testrunPass, string message) {
            var testrunMessage = string.Format("\nTestrun {0}. {1}", (testrunPass) ? "succeeded" : "failed", message);
            _outputWriter.Write(testrunMessage);
            _outputWriter.Write(string.Format("{0} tests passed", _passedCounter));
            _outputWriter.Write(string.Format("{0} tests failed", _failedCounter));
            return testrunPass;
        }
    }
}