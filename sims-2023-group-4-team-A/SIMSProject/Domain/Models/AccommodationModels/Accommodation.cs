using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
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
        public AccommodationType Type { get; set; } = AccommodationType.None;
        public int MaxGuestNumber { get; set; } = 2;
        public int MinReservationDays { get; set; } = 1;
        public int CancellationThreshold { get; set; } = 1;
        public List<string> ImageURLs { get; set; } = new();
        public string ImageURLsCSV { get; set; } = string.Empty;
        public string FeaturedImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsInRenovation { get; set; } = false;
        public bool IsRecentlyRenovated { get; set; } = false;
        public double Rating { get; set; } = 0;
        public int NumberOfRatings { get; set; } = 0;

        public Accommodation()
        {
        }

        public static AccommodationType GetType(string type)
        {
            return type switch
            {
                "Apartman" => AccommodationType.Apartment,
                "Kuća" => AccommodationType.House,
                "Koliba" => AccommodationType.Hut,
                _ => AccommodationType.None
            };
        }

        public static string GetType(AccommodationType type)
        {
            return type switch
            {
                AccommodationType.Apartment => "Apartman",
                AccommodationType.House => "Kuća",
                AccommodationType.Hut => "Koliba",
                _ => string.Empty
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
                ImageURLsCSV,
                Description,
                IsInRenovation.ToString(),
                IsRecentlyRenovated.ToString(),
                Math.Round(Rating, 2).ToString(),
                NumberOfRatings.ToString(),
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
            Description = values[i++];
            IsInRenovation = bool.Parse(values[i++]);
            IsRecentlyRenovated = bool.Parse(values[i++]);
            Rating = double.Parse(values[i++]);
            NumberOfRatings = int.Parse(values[i++]);
        }

        public override string? ToString()
        {
            return $"{GetType(Type)}: {Name} ({Location})";
        }

        public string ToStringSearchable { get => $"{Type} {Name} {Location} {GetType(Type)}"; }
    }
}