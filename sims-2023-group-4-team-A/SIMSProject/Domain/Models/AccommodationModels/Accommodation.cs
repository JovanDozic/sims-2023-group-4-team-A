using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Accommodation : ImageSerializer, ISerializable
    {
        public int Id { get; set; }
        public Owner Owner { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public Location Location { get; set; } = new();
        public AccommodationType Type { get; set; }
        public int MaxGuestNumber { get; set; } = 1;
        public int MinReservationDays { get; set; } = 1;
        public int CancellationThreshold { get; set; } = 1;
        public List<string> ImageURLs { get; set; } = new();
        public string ImageURLsCSV { get; set; } = string.Empty;
        public string FeaturedImage { get; set; } = string.Empty;

        public Accommodation()
        {
        }

        public static AccommodationType GetType(string type)
        {
            return type switch
            {
                "Apartman" => AccommodationType.Apartment,
                "Kuća" => AccommodationType.House,
                _ => AccommodationType.Hut
            };
        }

        public static string GetType(AccommodationType type)
        {
            return type switch
            {
                AccommodationType.Apartment => "Apartman",
                AccommodationType.House => "Kuća",
                _ => "Koliba"
            };
        }

        public string[] ToCSV()
        {
            ImageURLsCSV = ImageURLsToCSV(ImageURLs);
            string[] csvValues =
            {
                Id.ToString(),
                Owner.Id.ToString(),
                Name,
                Location.Id.ToString(),
                GetType(Type),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                CancellationThreshold.ToString(),
                ImageURLsCSV
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            var i = 0;
            Id = int.Parse(values[i++]);
            Owner.Id = int.Parse(values[i++]);
            Name = values[i++];
            Location.Id = int.Parse(values[i++]);
            Type = GetType(values[i++]);
            MaxGuestNumber = int.Parse(values[i++]);
            MinReservationDays = int.Parse(values[i++]);
            CancellationThreshold = int.Parse(values[i++]);
            ImageURLsCSV = values[i++];
            ImageURLs = ImageURLsFromCSV(ImageURLsCSV);
            FeaturedImage = ImageURLs.Count > 0 ? ImageURLs.First() : string.Empty;
        }

        public override string? ToString()
        {
            return $"{GetType(Type)}: {Name} ({Location})";
        }
    }
}