using System;
using System.Collections.Generic;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.UserModels
{
    public class Owner : User, ISerializable
    {
        public double Rating { get; set; }
        public List<Accommodation> Accommodations { get; set; } = new();

        public Owner()
        {
        }

        public Owner(int id, string username, string password,DateTime birthday ,double rating = 0)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = UserRole.Owner;
            Rating = rating;
            Birthday = birthday;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Username,
                Password,
                GetRole(Role),
                Math.Round(Rating, 2).ToString(),
                Birthday.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            Role = GetRole(values[3]);
            Rating = double.Parse(values[4]);
            Birthday = DateTime.Parse(values[5]);
        }

        public override string? ToString()
        {
            return Username;
        }
    }
}