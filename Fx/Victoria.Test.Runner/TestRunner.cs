using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Victoria.Test.Exceptions;

namespace Victoria.Test.Runner {
    public class TestRunner {

        private int _passedCounter;
        private int _failedCounter;

        public bool Execute(string testPath) {
            try {

                LoadTestAssemblies();

                var methods = GetTestMethods(testPath);
                if (methods.Count() == 0) return ExitRun(false, "Couldn't find any matching test methods");

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
                    }
                    catch (Exception ex) {
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

        private static IEnumerable<Assembly> _testAssemblies;

        private static void LoadTestAssemblies() {
            _testAssemblies = new List<Assembly> {
                Assembly.Load("Driverslog.Tests.Unit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"),
                Assembly.Load("Victoria.Test.Tests.Unit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")
            };
        }

        private static IEnumerable<MemberInfo> GetTestMethods(string testPath) {

            var methods = new List<MemberInfo>();
           
            if (testPath.IsMethod()) {
                methods.Add(GetTestMethod(testPath));
            } else if (testPath.IsRootNamespace()) {
                methods.AddRange(GetTestMethodsInRootNamespace(testPath));
            } else if (testPath.IsBlank()) {
                methods.AddRange(GetAllTestMethods());
            }

            return methods;
        }

        private static MemberInfo GetTestMethod(string testPath) {
            
            Console.WriteLine("Getting single method: " + testPath);

            var typeSegments    = testPath.Split('.');
            var methodName      = typeSegments.Last();
            var declaringType   = typeSegments[typeSegments.Count() - 2];
            var declaringAssembly = testPath.Substring(0, testPath.IndexOf("Unit")+4);
            var testAssembly    = _testAssemblies.Where(a => a.FullName.Contains(declaringAssembly)).Single();

            var testClass = testAssembly
                .GetExportedTypes()
                .Where(t => t.Name == declaringType)
                .Single();

            return GetTestMethodsInClass(testClass)
                .Where(s => s.Name == methodName)
                .Single();
        }

        private static IEnumerable<MemberInfo> GetAllTestMethods() {

            Console.WriteLine("Getting all methods");

            var methods = new List<MemberInfo>();

            foreach (var assembly in _testAssemblies) {
                var testClasses = assembly.GetExportedTypes().Where(t => t.Name.EndsWith("Tests"));
                LoadTestMethodsFromClasses(testClasses, methods);
            }

            return methods;
        }

        private static IEnumerable<MemberInfo> GetTestMethodsInRootNamespace(string testPath) {
            
            Console.WriteLine("Getting all test methods in root namespace: " + testPath);

            var testAssembly    = _testAssemblies.Where(a => a.FullName.Contains(testPath)).Single();
            var testClasses     = testAssembly.GetExportedTypes().Where(t => t.IsClass);

            var methods = new List<MemberInfo>();
            LoadTestMethodsFromClasses(testClasses,methods);
            return methods;
        }

        private static void LoadTestMethodsFromClasses(IEnumerable<Type> testClasses, List<MemberInfo> methods) {
            foreach (var testClass in testClasses) {
                methods.AddRange(GetTestMethodsInClass(testClass));
            }
        }

        private static IEnumerable<MemberInfo> GetTestMethodsInClass(Type testClass) {
            return testClass
                .FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance, null, null)
                .Where(s => s.IsMarkedWith<FactAttribute>());
        }
    }
}