﻿using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum Language { ENGLISH = 0, SERBIAN, SPANISH, FRENCH };

namespace SIMSProject.Model
{
    public class Tour : ISerializable
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public User Guide { get; set; }

        public TourLocation Location {get; set;}
        public string Description { get; set; } = string.Empty;

        public Language TourLanguage {get; set;}

        public int  MaxGuestNumber { get; set; }

        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();

        public List<TourDate> Dates { get; set; } = new List<TourDate>();
        public int Duration { get; set; }

        public List<String> Images { get; set; } = new List<String>();

        public List<User> Sightseers { get; set; } = new List<User>();

        public int LocationId { get; set; }
        public int GuideId { get; set; }

        public Tour(){}

        public Tour(int id, string name, User guide, TourLocation location, string description, Language tourLanguage, int maxGuestNumber, int duration, int locationId, int guideId)
        {
            Id = id;
            Name = name;
            Guide = guide;
            Location = location;
            Description = description;
            TourLanguage = tourLanguage;
            MaxGuestNumber = maxGuestNumber;
            Duration = duration;
            LocationId = locationId;
            GuideId = guideId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            Enum.TryParse(values[3], out Language TourLanguage);
            MaxGuestNumber = Convert.ToInt32(values[4]);
            Duration = Convert.ToInt32(values[5]);
            LocationId = Convert.ToInt32(values[6]);
            GuideId = Convert.ToInt32(values[7]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), Name, Description, TourLanguage.ToString(), MaxGuestNumber.ToString(), Duration.ToString(), LocationId.ToString(), GuideId.ToString() };
            return csvValues;
        }
    }
}
