using System;
using System.Globalization;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class AccommodationStatistic
    {
        public Accommodation Accommodation { get; set; } = new();
        public AccommodationStatisticType Type { get; set; }
        public int Year { get; set; } = 0;
        public int Month { get; set; } = 0; // If Year is >= 0, and Month is 0, it's Yearly statistic (but Type is there anyways)

        public string ShortYear { get => $"'{Year.ToString()[2..]}"; }
        public string ShortMonth 
        { 
            get
            { 
                var monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month).Substring(0, 3) + "."; 
                return char.ToUpper(monthName[0]) + monthName.Substring(1);
            }
        }
        public int TotalReservations { get; set; } = 0;
        public int CancelledReservations { get; set; } = 0;
        public int RescheduledReservations { get; set; } = 0;
        public int RenovationRecommendations { get; set; } = 0;

        public AccommodationStatistic() { }

    }
}
