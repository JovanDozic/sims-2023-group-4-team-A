using System;

namespace SIMSProject.Domain.Models
{
    public class DateRange
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FormattedDateRange => $"{StartDate:dd/MM/yyyy} - {EndDate:dd/MM/yyyy}";

        public DateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public DateRange()
        {

        }

        public override string ToString()
        {
            return FormattedDateRange;
        }
    }
}
