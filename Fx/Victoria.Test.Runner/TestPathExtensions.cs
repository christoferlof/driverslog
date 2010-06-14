using System.Linq;

namespace Victoria.Test.Runner {
    public static class TestPathExtensions {
        public static bool IsMethod(this string testPath) {
            return (!IsBlank(testPath) && !IsRootNamespace(testPath));
        }

        public static bool IsRootNamespace(this string testPath) {
            return testPath.EndsWith("Tests.Unit");
        }

        public static bool IsBlank(this string testPath) {
            return string.IsNullOrEmpty(testPath);
        }

        public static string GetTestClassName(this string testPath) {
            return testPath.Substring(0, testPath.LastIndexOf('.'));
        }
    }
}