using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Victoria.Test.Exceptions;
using Victoria.Test.Runner;

namespace Victoria.Test.Tests.Unit.Runner {
    public class TestRunnerTests {
    
        [Fact]
        public void should_return_success_when_all_tests_pass() {
            var methodResolver = new TestMethodResolver(new FakeAssemblyResolver());
            var runner = new TestRunner(methodResolver);
            var testMethod = "Victoria.Test.Tests.Unit.AssertTests.TrueThrows";
            var result = runner.Execute(testMethod);

            Assert.True(result);
        }

        [Fact]
        public void should_return_failure_when_a_single_test_fails() {
            var methodResolver = new FakeMethodResolver();
            var runner = new TestRunner(methodResolver);
            var testMethod = "Victoria.Test.Tests.Unit.Runner.TestRunnerTests.this_is_a_failing_test_for_testing";
            var result = runner.Execute(testMethod);

            Assert.True(!result);
        }

        public void this_is_a_failing_test_for_testing() {
            throw new TrueException();
        }

        public class FakeAssemblyResolver : TestAssemblyResolver {
            public override IEnumerable<string> GetTestAssemblies() {
                return new []{GetType().Assembly.FullName};
            }
        }

        public class FakeMethodResolver : TestMethodResolver {
            
            public FakeMethodResolver() : this(new FakeAssemblyResolver()){}
            
            public FakeMethodResolver(TestAssemblyResolver testAssemblyResolver) 
                : base(testAssemblyResolver) {
            }

            public override MemberInfo GetTestMethod(string testPath) {
                return typeof(TestRunnerTests)
                    .FindMembers(MemberTypes.Method, BindingFlags.Public | BindingFlags.Instance, null, null)
                    .Where(n => n.Name == "this_is_a_failing_test_for_testing")
                    .SingleOrDefault();
            }
        }
    }
}