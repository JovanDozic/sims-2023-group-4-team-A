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
    public class VoucherObtainingReasonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObtainingReason reason)
            {
                switch (reason)
                {
                    case ObtainingReason.APPOINTMENTCANCELED:
                        return "Otkazivanje ture";
                    case ObtainingReason.GUIDEQUIT:
                        return "Otkaz vodiča";
                    case ObtainingReason.WON:
                        return "5+1 gratis";
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
