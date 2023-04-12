using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class Accommodation : ISerializable
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

        public Accommodation()
        {
        }

        public Accommodation(int ownerId, string name, Location location, AccommodationType type, int maxGuestNumber,
            int minReservationDays, int cancellationThreshold, string imageURLsCSV)
        {
            Owner.Id = ownerId;
            Name = name;
            Location = location;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            CancellationThreshold = cancellationThreshold;
            ImageURLs = new List<string>();
            ImageURLsCSV = imageURLsCSV;
            ImageURLsFromCSV(ImageURLsCSV);
        }

        public string[] ToCSV()
        {
            ImageURLsToCSV();
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
            ImageURLsFromCSV(ImageURLsCSV);
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

        public void ImageURLsToCSV()
        {
            if (ImageURLs.Count > 0)
            {
                ImageURLsCSV = string.Empty;
                foreach (var imageURL in ImageURLs)
                {
                    ImageURLsCSV += imageURL + ",";
                }

                ImageURLsCSV = ImageURLsCSV.Remove(ImageURLsCSV.Length - 1);
            }
        }

        public void ImageURLsFromCSV(string value)
        {
            var imageURLs = value.Split(',');
            foreach (var imageURL in imageURLs)
            {
                if (imageURL != string.Empty)
                {
                    ImageURLs.Add(imageURL);
                }
            }
        }

        public override string? ToString()
        {
            return Type + ": " + Name + " (" + Location + ")";
        }
    }
}