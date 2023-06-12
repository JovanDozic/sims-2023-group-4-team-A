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

                var app = (App)System.Windows.Application.Current;
                if (app.CurrentLanguage == "en-US")
                {
                    return type switch
                    {
                        AccommodationType.Apartment => "Apartment",
                        AccommodationType.House => "House",
                        AccommodationType.Hut => "Hut",
                        _ => "Select..."
                    };
                }

                return type switch
                {
                    AccommodationType.Apartment => "Apartman",
                    AccommodationType.House => "Kuća",
                    AccommodationType.Hut => "Koliba",
                    _ => "Izaberite..."
                };
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
