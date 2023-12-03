using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoriesBloom
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string sValue)
            {
                if (string.IsNullOrEmpty(sValue) || sValue == null)
                    return false;
            }

            return (bool)value ? 1 : 0;
        }
    }
}
