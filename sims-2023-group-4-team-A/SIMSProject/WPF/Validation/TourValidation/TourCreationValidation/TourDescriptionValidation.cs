using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SIMSProject.WPF.Validation.TourValidation.TourCreationValidation
{
    public class TourDescriptionValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                var s = value as string;
                if (s.Equals(string.Empty))
                {
                    return new ValidationResult(false, "Opis je obavezan");
                }
                if (s.Split().Length < 3)
                {
                    return new ValidationResult(false, "Opis se mora sastojati iz bar 3 reči");
                }
                return new ValidationResult(true, null);
            }
            catch
            {
                return new ValidationResult(false, "Unknown error occured.");
            }
            
        }
    }
}
