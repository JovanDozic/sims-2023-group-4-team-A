using System;
using System.Collections.Generic;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;


namespace SIMSProject.Domain.Models.UserModels
{
    public class Guide : User, ISerializable
    {
        public double Rating { get; set; }
        public List<Tour> Tours { get; set; } = new();
        public List<AccommodationReservation> AccommodationReservations { get; set; } = new();

        public Guide()
        {
        }

        public Guide(int id, string username, string password, double rating)
        {
            Id = id;
            Username = username;
            Password = password;
            Rating = rating;
            Role = UserRole.Guide;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"{Username}";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Username,
                Password,
                GetRole(Role),
                Math.Round(Rating, 2).ToString()
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
        }
    }
}