using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SIMSProject.WPF.Converters
{
    public class AccommodationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is AccommodationType type)
            {
                var returnValue = Accommodation.GetType(type);
                return returnValue == string.Empty ? "Izaberite..." : returnValue;
            }
            return "<null>";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not in use
            return null;
        }
    }
}
