using System;
using System.Collections.Generic;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.UserModels
{
    public class Guest : User, ISerializable
    {
        public double Rating { get; set; }
        public List<TourReservation> TourReservations { get; set; } = new();
        public List<Voucher> Vouchers { get; set; } = new();

        public Guest()
        {
        }

        public Guest(int id, string username, string password, double rating = 0)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = UserRole.Guest;
            Rating = rating;
            TourReservations = new List<TourReservation>();
            Vouchers = new List<Voucher>();
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