using System;
using System.Globalization;
using System.Windows.Data;

namespace UILibrary.Converters
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class MultipleBoolsToBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (bool value in values)
            {
                if (value == false)
                {
                    return false;
                }
            }
            return true;
        }

        public object[] ConvertBack(object values, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
