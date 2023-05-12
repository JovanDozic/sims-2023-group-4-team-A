using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using SIMSProject.FileHandlers;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;


namespace SIMSProject.Domain.Models.TourModels
{
    public enum Language { ENGLISH = 1, SERBIAN, SPANISH, FRENCH };

    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Language TourLanguage { get; set; }
        public int MaxGuestNumber { get; set; }
        public int Duration { get; set; }
        public Location Location { get; set; } = new();
        public Guide Guide { get; set; } = new();
        public List<TourAppointment> Appointments { get; set; } = new();
        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();
        public List<string> Images { get; set; } = new List<string>();

        public Tour() { }

        public Tour(string name, int locationId, string description, Language tourLanguage, int maxGuestNumber, int duration, int guideId)
        {
            Name = name;
            Location.Id = locationId;
            Description = description;
            Duration = duration;
            TourLanguage = tourLanguage;
            MaxGuestNumber = maxGuestNumber;
            Location.Id = locationId;
            Guide.Id = guideId;
        }

        public static string GetLanguage(Language language)
        {
            return language switch
            {
                Language.ENGLISH => "Engleski",
                Language.FRENCH => "Francuski",
                Language.SERBIAN => "Srpski",
                Language.SPANISH => "Španski",
                _ => "Jezik"
            };
        }

        public string CreateImageURLs()
        {
            StringBuilder imageURLs = new();
            foreach (string imageURL in Images)
            {
                imageURLs.Append(imageURL + ",");
            }
            imageURLs.Remove(imageURLs.Length - 1, 1);
            return imageURLs.ToString();
        }

        public override string? ToString()
        {
            return Name + ", " + Location + ", " + TourLanguage.ToString();
        }

        public string KeyPointsToString()
        {
            var builder = new StringBuilder();
            foreach (var keyPoint in KeyPoints)
            {
                builder.Append($"{keyPoint.ToString()}\n");
            }
            return builder.ToString();
        }

        public string ToStringSearch()
        {
            return Location + " " + TourLanguage.ToString();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                Description,
                TourLanguage.ToString(),
                MaxGuestNumber.ToString(),
                Duration.ToString(),
                Location.Id.ToString(),
                Guide.Id.ToString(),
                CreateImageURLs()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            TourLanguage = (Language)Enum.Parse(typeof(Language), values[3]);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            Duration = Convert.ToInt32(values[5]);
            Location.Id = Convert.ToInt32(values[6]);
            Guide.Id = Convert.ToInt32(values[7]);
            Images.AddRange(values[8].Split(','));
        }
    }
}
