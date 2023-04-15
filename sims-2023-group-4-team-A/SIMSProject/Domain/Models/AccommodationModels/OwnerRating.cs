using SIMSProject.Serializer;
using System.Collections.Generic;
using System.Globalization;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class OwnerRating : ImageSerializer, ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; } = new();
        public int CleanlinessRating { get; set; } = 1;
        public int OwnerCorrectness { get; set; } = 1;
        public int Kindness { get; set; } = 1;
        public double Overall { get => CalculateOverall(); set { } }
        public string Comment { get; set; } = string.Empty;
        public List<string> ImageURLs { get; set; } = new();
        public string ImageURLsCSV { get; set; } = string.Empty;

        public OwnerRating()
        {
        }

        private double CalculateOverall()
        {
            return (CleanlinessRating + OwnerCorrectness + Kindness) / (double)3;
        }

        public string[] ToCSV()
        {
            ImageURLsCSV = ImageURLsToCSV(ImageURLs);
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                CleanlinessRating.ToString(),
                OwnerCorrectness.ToString(),
                Kindness.ToString(),
                Overall.ToString(CultureInfo.CurrentCulture),
                Comment,
                ImageURLsCSV
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Reservation.Id = int.Parse(values[1]);
            CleanlinessRating = int.Parse(values[2]);
            OwnerCorrectness = int.Parse(values[3]);
            Kindness = int.Parse(values[4]);
            Overall = double.Parse(values[5]);
            Comment = values[6];
            ImageURLsCSV = values[7];
            ImageURLs = ImageURLsFromCSV(ImageURLsCSV);
        }

        public override string? ToString()
        {
            return "Ocena za vlasnika <" + Id + ">: " + Overall + " (" + Comment + "), " + ImageURLs.Count + " slike";
        }
    }
}
