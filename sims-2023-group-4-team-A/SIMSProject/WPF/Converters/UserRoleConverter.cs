using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Globalization;
using System.Windows.Data;

namespace SIMSProject.WPF.Converters
{
    public class UserRoleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is UserRole type)
                return User.GetRole(type);
            return "<null>";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not in use
            return null;
        }
    }
}