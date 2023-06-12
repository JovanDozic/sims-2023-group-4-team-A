using SIMSProject.Serializer;
using System;
using System.Globalization;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class AccommodationRenovation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; } = new();
        public DateTime StartDate { get; set;  } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public int NumberOfDays { get; set; } = 1;
        public string Description { get; set; } = string.Empty;
        public bool IsCancelled { get; set; } = false;
        public string FormattedStartDate
        {
            get => StartDate.ToString("ddd, d. MMM. yyyy.");
        }
        public string FormattedEndDate
        {
            get => EndDate.ToString("ddd, d. MMM. yyyy.");
        }
        public bool CanCancelRenovation
        {
            get => (StartDate - DateTime.Now).TotalDays > 5 && !IsCancelled;
        }
        public string CanCancelRenovationIcon
        {
            get
            {
                if (IsCancelled) return "None";
                else if (EndDate < DateTime.Now) return "None";
                return CanCancelRenovation ? "XxIcon" : "XxDisabledIcon";
            }
        }

        public AccommodationRenovation() { }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                StartDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                EndDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                NumberOfDays.ToString(),    
                Description,
                IsCancelled.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            Accommodation.Id = int.Parse(values[i++]);
            StartDate = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
            EndDate = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
            NumberOfDays = int.Parse(values[i++]);
            Description = values[i++];
            IsCancelled = bool.Parse(values[i++]);
        }

        public override string ToString()
        {
            return $"{Accommodation} - {StartDate.ToString("dd.MM.yyyy")} - {EndDate.ToString("dd.MM.yyyy")}";
        }

    }
}
