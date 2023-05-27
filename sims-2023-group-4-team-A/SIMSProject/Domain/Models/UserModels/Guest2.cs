using System;
using System.Collections.Generic;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.UserModels
{
    public class Guest2 : User, ISerializable
    {
        public double Rating { get; set; }

        public Guest2()
        {
        }

        public Guest2(int id, string username, string password, DateTime birthday, double rating = 0)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = UserRole.Guest2;
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

        public override string ToString()
        {
            return $"{Username}";
        }
    }
}