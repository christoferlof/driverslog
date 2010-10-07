using System.Collections.Generic;
using System.Linq;

namespace Victoria.Test.Tests.Unit.Runner {
    public class TestAssemblyResolverTests {

        private IEnumerable<string> _assemblies;

        public TestAssemblyResolverTests() {
            var resolver = new TestAssemblyResolver();
            _assemblies = resolver.GetTestAssemblies();
        }

        [Fact]
        public void should_find_driverslog_assembly() {
            Assert.True(_assemblies.Any(x => x.Contains("Driverslog.Tests.Unit")));
        }

        [Fact]
        public void should_find_fx_assembly() {
            Assert.True(_assemblies.Any(x => x.Contains("Victoria.Test.Tests.Unit")));
        }
    }
}