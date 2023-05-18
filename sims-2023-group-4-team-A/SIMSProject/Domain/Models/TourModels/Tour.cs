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
    public enum CreatingReason { REGULAR = 0, CUSTOM, STATISTICS};
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Language TourLanguage { get; set; }
        public CreatingReason Reason { get; set; }
        public int MaxGuestNumber { get; set; }
        public int Duration { get; set; }
        public Location Location { get; set; } = new();
        public Guide Guide { get; set; } = new();
        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();
        public List<string> Images { get; set; } = new List<string>();

        public Tour() { }

        public Tour(string name, int locationId, string description, Language tourLanguage,  int maxGuestNumber, int duration, int guideId, CreatingReason reason, List<KeyPoint> keyPoints, List<string> images)
        {
            Name = name;
            Location.Id = locationId;
            Description = description;
            Duration = duration;
            TourLanguage = tourLanguage;
            MaxGuestNumber = maxGuestNumber;
            Location.Id = locationId;
            Guide.Id = guideId;
            Reason = reason;
            KeyPoints = keyPoints;
            Images = images;
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
        public static List<string> GetLanguages()
        {
            List<string> languages = new();
            foreach (Language language in Enum.GetValues(typeof(Language)))
            {
                languages.Add(GetLanguage(language));
            }
            return languages;
        }
        public static string GetReason(CreatingReason reason)
        {
            return reason switch
            {
                CreatingReason.STATISTICS => "Preko statistike",
                CreatingReason.REGULAR => "Obična tura",
                CreatingReason.CUSTOM => "Po želji",
                _ => "Razlog kreiranja"
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
        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                Description,
                TourLanguage.ToString(),
                Reason.ToString(),
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
            Reason = (CreatingReason)Enum.Parse(typeof(CreatingReason), values[4]);
            MaxGuestNumber = Convert.ToInt32(values[5]);
            Duration = Convert.ToInt32(values[6]);
            Location.Id = Convert.ToInt32(values[7]);
            Guide.Id = Convert.ToInt32(values[8]);
            Images.AddRange(values[9].Split(','));
        }
    }
}
