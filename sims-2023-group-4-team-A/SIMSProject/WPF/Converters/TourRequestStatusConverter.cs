using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SIMSProject.WPF.Converters
{
    public class TourRequestStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is RequestStatus status)
            {
                switch (status)
                {
                    case RequestStatus.ONHOLD:
                        return "Na čekanju";
                    case RequestStatus.INVALID:
                        return "Nevažeći";
                    case RequestStatus.ACCEPTED:
                        return "Prihvaćen";
                    default:
                        return string.Empty;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
