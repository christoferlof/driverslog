using Microsoft.Phone.Controls;

namespace Victoria.Test.Runner {
    public class IntegrationTestRunner : TestRunner {
        
        private readonly PhoneApplicationFrame _rootVisual;

        public IntegrationTestRunner(
            TestMethodResolver testMethodResolver, 
            OutputWriter outputWriter, 
            PhoneApplicationFrame rootVisual) : base(testMethodResolver, outputWriter) {
            
            _rootVisual = rootVisual;
        }

        protected override object CreateInstance(System.Type testClass) {
            var testObject = base.CreateInstance(testClass);
            
            if (ShouldAttachRootVisual(testObject)) {
                ((IntegrationTest)testObject).RootFrame = _rootVisual;
            }
            return testObject;
        }

        private bool ShouldAttachRootVisual(object testObject) {
            return testObject is IntegrationTest && _rootVisual != null;
        }
    }
}