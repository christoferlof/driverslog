using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Driverslog.Converters {
    public class YesNoToIndexConverter : IValueConverter {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return 0;
            Debug.WriteLine("Converting: " + value.ToString());
            if ((bool)value)
                return 1; //yes
            return 0; //no

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}