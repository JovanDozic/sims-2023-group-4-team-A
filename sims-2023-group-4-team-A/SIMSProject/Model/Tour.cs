using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Language { ENGLISH, SERBIAN };

namespace SIMSProject.Model
{
    public class Tour : ISerializable
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        //public Location Location {get; set;}
        public string Description { get; set; } = string.Empty;

        public Language TourLanguage {get; set;}

        public int  MaxGuestNumber { get; set; }

        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();

        public DateTime  StartDateTime { get; set; }

        public int Duration { get; set; }

        public List<String> Images { get; set; } = new List<String>();

        public int LocationId { get; set; }

        public Tour(){}

        public Tour(int id, string name, string description, Language tourLanguage, int maxGuestNumber, DateTime startDateTime, int duration, int locationId)
        {
            Id = id;
            Name = name;
            Description = description;
            TourLanguage = tourLanguage;
            MaxGuestNumber = maxGuestNumber;
            StartDateTime = startDateTime;
            Duration = duration;
            LocationId = locationId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            Enum.TryParse(values[3], out Language TourLanguage);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            DateTime.TryParse(values[5], out DateTime StartDateTime);
            Duration = Convert.ToInt32(values[6]);
            LocationId = Convert.ToInt32(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), Name, Description, TourLanguage.ToString(), MaxGuestNumber.ToString(), StartDateTime.ToString(), Duration.ToString(), LocationId.ToString() };
            return csvValues;
        }
    }
}
