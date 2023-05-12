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
    public class TourLanguageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Language language)
            {
                switch (language)
                {
                    case Language.ENGLISH:
                        return "Engleski";
                    case Language.SERBIAN:
                        return "Srpski";
                    case Language.SPANISH:
                        return "Španski";
                    case Language.FRENCH:
                        return "Francuski";
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
