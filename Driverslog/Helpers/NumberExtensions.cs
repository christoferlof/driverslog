using System.Globalization;

namespace Driverslog.Helpers {
    public static class NumberExtensions {
        
        public static int AsInt(this string self) {
            var output = 0;
            return int.TryParse(self,NumberStyles.Integer,CultureInfo.CurrentUICulture,out output) ? output : 0;
        }

        public static string AsString(this int self) {
            return (self == 0) ? string.Empty : self.ToString(CultureInfo.CurrentUICulture);
        }

    }
}