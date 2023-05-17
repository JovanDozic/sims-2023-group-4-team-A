using System;

namespace SIMSProject.Application.Services.AccommodationServices
{
    public class AccommodationRating
    {
        public double CleanlinessRating { get; set; } = 0;
        public double OwnerCorrectness { get; set; } = 0;
        public double Kindness { get; set; } = 0;
        public double Overall { get => Math.Round(CalculateOverall(), 2); set { } }
        public int NumberOfRatings { get; set; } = 0;

        public AccommodationRating()
        {

        }

        private double CalculateOverall()
        {
            return (CleanlinessRating + OwnerCorrectness + Kindness) / 3;
        }

        public override string? ToString()
        {
            return $"Overall: {Overall}\nCleanliness: {CleanlinessRating}\nCorrectness: {OwnerCorrectness}\nKindness: {Kindness}\nNumber of ratings: {NumberOfRatings}";
        }
    }
}
