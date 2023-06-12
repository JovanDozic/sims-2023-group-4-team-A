using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public int MaxGuestNumber { get; set; } = 1;
        public int MinReservationDays { get; set; } = 1;
        public int CancellationThreshold { get; set; } = 1;
        public List<string> ImageURLs { get; set; } = new();
        public string ImageURLsCSV { get; set; } = string.Empty;
        public string FeaturedImage { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsInRenovation { get; set; } = false;
        public bool IsRecentlyRenovated { get; set; } = false;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public AccommodationRating Rating { get; set; } = new();
        public bool IsOwnerSuper => Owner.Role == UserRole.SuperOwner;
        public string ToStringSearchable { get => $"{Type} {Name} {Location} {GetType(Type)}"; }
        public string NameAndLocation { get => $"{Name} ({Location})"; }

        public Accommodation()
        {
        }

        public Accommodation(Accommodation original)
        {
            if (original is null) return;
            Id = original.Id;
            Owner = original.Owner;
            Name = original.Name;
            Location = original.Location;
            Type = original.Type;
            MaxGuestNumber = original.MaxGuestNumber;
            MinReservationDays = original.MinReservationDays;
            CancellationThreshold = original.CancellationThreshold;
            ImageURLs = new List<string>(original.ImageURLs);
            ImageURLsCSV = original.ImageURLsCSV;
            FeaturedImage = original.FeaturedImage;
            Description = original.Description;
            IsInRenovation = original.IsInRenovation;
            IsRecentlyRenovated = original.IsRecentlyRenovated;
            Rating = original.Rating;
            DateCreated = original.DateCreated;
            // TODO: Check if something is missing
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
                Description,
                IsInRenovation.ToString(),
                IsRecentlyRenovated.ToString(),
                Math.Round(Rating.Overall, 2).ToString(),
                Rating.NumberOfRatings.ToString(),
                DateCreated.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
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
            Description = values[i++];
            IsInRenovation = bool.Parse(values[i++]);
            IsRecentlyRenovated = bool.Parse(values[i++]);
            Rating.Overall = double.Parse(values[i++]);
            Rating.NumberOfRatings = int.Parse(values[i++]);
            DateCreated = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
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