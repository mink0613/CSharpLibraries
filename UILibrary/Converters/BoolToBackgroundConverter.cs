using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace UILibrary.Converters
{
    [ValueConversion(typeof(bool), typeof(SolidColorBrush))]
    public class BoolToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return (SolidColorBrush)(new BrushConverter().ConvertFrom("#008000"));
            }

            return (SolidColorBrush)(new BrushConverter().ConvertFrom("#d9d9d9"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
