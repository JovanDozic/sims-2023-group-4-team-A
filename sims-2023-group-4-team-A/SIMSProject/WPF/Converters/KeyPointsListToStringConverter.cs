using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SIMSProject.WPF.Converters
{
    public class KeyPointsListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<KeyPoint> keyPoints)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var keyPoint in keyPoints)
                {
                    stringBuilder.Append(keyPoint.Description);
                    stringBuilder.Append(", ");
                }

                // Ukloni poslednji zarez i razmak ako postoji
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Length -= 2;
                }

                return stringBuilder.ToString();
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
