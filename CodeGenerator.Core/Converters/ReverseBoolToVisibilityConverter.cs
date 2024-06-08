using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CodeGenerator.Core.Converters
{
    public class ReverseBoolToVisibilityConverter : IValueConverter
    {
        

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
            {
                return !val ? Visibility.Visible : Visibility.Collapsed;
            }

            throw new ArgumentException("value应该是bool类型");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility val)
            {
                return val != Visibility.Visible ? true : false;
            }
            throw new ArgumentException("value应该是Visibility类型");

        }
    }
}
