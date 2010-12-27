using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Driverslog.Helpers {
    public class TypeToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (value.GetType().Name == (string)parameter) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}