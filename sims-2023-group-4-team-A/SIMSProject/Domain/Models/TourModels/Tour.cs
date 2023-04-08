using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using SIMSProject.Controller;
using SIMSProject.FileHandler;
using SIMSProject.Model.UserModel;
using SIMSProject.Model;


namespace SIMSProject.Domain.Models.TourModels
{
    public enum Language { ENGLISH = 0, SERBIAN, SPANISH, FRENCH };

    public class Tour
    {
        public int Id;
        public int GuideId;
        public int LocationId;

        public string Name = string.Empty;
        public string Description = string.Empty;
        public Language TourLanguage;
        public int MaxGuestNumber;
        public int Duration;

        public Guide Guide { get; set; } = new();
        public Location Location { get; set; } = new();
        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();
        public List<TourAppointment> Appointments { get; set; } = new List<TourAppointment>();
        public List<string> Images { get; set; } = new List<string>();

        public Tour() { }

        public Tour(string name, Guide guide, Location location, string description, Language tourLanguage, int maxGuestNumber, int duration, int locationId, int guideId)
        {
            Name = name;
            Guide = guide;
            Location = location;
            Description = description;
            Duration = duration;
            TourLanguage = tourLanguage;
            MaxGuestNumber = maxGuestNumber;
            LocationId = locationId;
            GuideId = guideId;
        }
        
        public StringBuilder CreateImageURLs()
        {
            StringBuilder imageURLs = new();
            foreach (string imageURL in Images)
            {
                imageURLs.Append(imageURL + ",");
            }
            imageURLs.Remove(imageURLs.Length - 1, 1);
            return imageURLs;
        }

        /*Validation*/
        readonly Regex NameReg = new("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$");
        readonly Regex DescriptionReg = new("^\\w+(\\s+\\w+){2,}$");
        public override string? ToString()
        {
            return Name + ", " + Location + ", " + TourLanguage.ToString();
        }

        public string ToStringSearch()
        {
            return Location + " " + TourLanguage.ToString();
        }
    }
}
