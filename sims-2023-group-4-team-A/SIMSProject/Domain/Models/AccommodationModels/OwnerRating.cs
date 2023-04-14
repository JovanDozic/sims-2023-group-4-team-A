using SIMSProject.Serializer;
using System.Collections.Generic;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class OwnerRating : ImageSerializer, ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; } = new();
        public int CleanlinessRating { get; set; } = 1;
        public int OwnerCorrectness { get; set; } = 1;
        public int Kindness { get; set; } = 1;
        public string Comment { get; set; } = string.Empty;
        public List<string> ImageURLs { get; set; } = new();
        public string ImageURLsCSV { get; set; } = string.Empty;

        public OwnerRating()
        {
        }

        public OwnerRating(int id, AccommodationReservation accommodationReservation, int cleanlinessRating, int ownerCorrectness, string comment, List<string> imageURLs, string imageURLsCSV)
        {
            Id = id;
            AccommodationReservation = accommodationReservation;
            CleanlinessRating = cleanlinessRating;
            OwnerCorrectness = ownerCorrectness;
            Comment = comment;
            ImageURLs = imageURLs;
            ImageURLsCSV = imageURLsCSV;
        }

        public string[] ToCSV()
        {
            ImageURLsCSV = ImageURLsToCSV(ImageURLs);
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                CleanlinessRating.ToString(),
                OwnerCorrectness.ToString(),
                Comment,
                ImageURLsCSV
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservation.Id = int.Parse(values[1]);
            CleanlinessRating = int.Parse(values[2]);
            OwnerCorrectness = int.Parse(values[3]);
            Comment = values[4];
            ImageURLsCSV = values[5];
            ImageURLs = ImageURLsFromCSV(ImageURLsCSV);
        }
    }
}
