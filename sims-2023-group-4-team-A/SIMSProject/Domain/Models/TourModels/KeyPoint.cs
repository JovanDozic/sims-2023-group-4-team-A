using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace SIMSProject.Domain.Models.TourModels
{
    public class KeyPoint : ISerializable
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public Location Location { get; set; } = new();

        public KeyPoint()
        {
        }
        public KeyPoint(int id, string description, int locationId)
        {
            Id = id;
            Description = description;
            Location.Id = locationId;
        }

        public override string ToString()
        {
            return  Description;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Description, Location.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Description = values[1];
            Location.Id = Convert.ToInt32(values[2]);
        }
    }
}
