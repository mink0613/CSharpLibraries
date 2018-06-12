using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace UILibrary.Converters
{
    [ValueConversion(typeof(object), typeof(double))]
    public class WidthConverter : IValueConverter
    {
        public object Convert(object o, Type type, object parameter, CultureInfo culture)
        {
            ListView l = o as ListView;
            Grid parent = l.Parent as Grid;
            if (parent == null)
            {
                return 0;
            }

            UserControl view = parent.Parent as UserControl;
            if (view == null)
            {
                return 0;
            }

            double parentWidth = view.Width;
            Thickness margin = l.Margin;
            double leftRightMargin = margin.Left + margin.Right;

            GridView g = l.View as GridView;
            double total = 0;
            for (int i = 1; i < g.Columns.Count; i++)
            {
                total += g.Columns[i].Width;
            }
            return (parentWidth - leftRightMargin - total);
        }

        public object ConvertBack(object o, Type type, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
