using System;
using System.Globalization;
using System.Windows.Data;

namespace SIMSProject.WPF.Converters
{
    public class MultiplyConverter : IValueConverter
    {
        public double Factor { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double)value * Factor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
