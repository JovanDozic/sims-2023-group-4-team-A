using SIMSProject.Domain.Models.TourModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SIMSProject.WPF.Converters
{
    public class VoucherExpirationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Voucher voucher)
            {
                string expirationDate = voucher.Expiration.ToString("dd.MM.yyyy.");
                return $"{expirationDate}";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}