using System;
using System.Globalization;
using System.Windows.Data;
using SIMSProject.Domain.Models;
using SIMSProject.Model;

namespace SIMSProject.WPF.Converters
{
    internal class RequestsStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ReschedulingRequestStatus type)
                return ReschedulingRequest.GetStatus(type);
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not in use
            return null;
        }
    }
}
