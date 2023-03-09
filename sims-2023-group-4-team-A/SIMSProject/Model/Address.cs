using System;
using System.Collections.Generic;
using System.Linq;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    public class Address : ISerializable
    {
        public int Id { get; set; }
        public string Street { get; set; } = string.Empty;
        public string StreetNumber { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public Address()
        {
        }

        public Address(int id, string street, string streetNumber, string city, string country)
        {
            Id = id;
            Street = street;
            StreetNumber = streetNumber;
            City = city;
            Country = country;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Street, StreetNumber, City, Country };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Street = values[1];
            StreetNumber = values[2];
            City = values[3];
            Country = values[4];
        }
    }
}
