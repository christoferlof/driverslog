using System.Collections.Generic;

namespace Victoria.Test.Tests.Unit.Runner {
    public class TestAssemblyResolver {
        public virtual IEnumerable<string> GetTestAssemblies() {
            return new[] {
                "Driverslog.Tests.Unit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null",
                "Victoria.Test.Tests.Unit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"
            };
        }
    }
}