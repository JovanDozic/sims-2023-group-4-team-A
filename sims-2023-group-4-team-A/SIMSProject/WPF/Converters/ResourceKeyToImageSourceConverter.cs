using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SIMSProject.WPF.Converters
{
    public class ResourceKeyToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string resourceKey = value as string;
            var app = (App)System.Windows.Application.Current;
            return app.Resources[resourceKey] as ImageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
