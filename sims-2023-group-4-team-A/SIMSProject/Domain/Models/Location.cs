using System;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models
{
    public class Location : ISerializable
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int? ForumsCount {get; set;} = null;

        public Location()
        {
        }

        public Location(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public Location(string city, string country)
        {
            Id = 0;
            City = city;
            Country = country;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { 
                Id.ToString(), 
                City, 
                Country 
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = Convert.ToString(values[1]);
            Country = Convert.ToString(values[2]);
        }

        public override string? ToString()
        {
            return $"{City}, {Country}";
        }
    }
}