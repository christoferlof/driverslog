using System.Globalization;
using System.Windows;
using Driverslog.Helpers;
using Victoria.Test;

namespace Driverslog.Tests.Unit.Helpers {
    public class TypeToVisibilityConverterTests {
        
        [Fact]
        public void Convert_MatchingType_Visible() {
            
            var converter = new TypeToVisibilityConverter();

            var result = (Visibility)converter.Convert("foo",null,"String",CultureInfo.InvariantCulture);

            Assert.Equal(Visibility.Visible,result);

        }

        [Fact]
        public void Convert_NonMatchingType_Collapsed() {
            
            var converter = new TypeToVisibilityConverter();

            var result = (Visibility)converter.Convert((int)0,null,"String",CultureInfo.InvariantCulture);

            Assert.Equal(Visibility.Collapsed,result);
        }

    }
}