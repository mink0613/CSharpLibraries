using System;
using System.Globalization;
using System.Windows.Data;

namespace UILibrary.Converters
{
    [ValueConversion(typeof(bool), typeof(string))]
    public class BoolToOnOffConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "On";
            }

            return "Off";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
