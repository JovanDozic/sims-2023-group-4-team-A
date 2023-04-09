using System;
using System.Collections.Generic;
using SIMSProject.Model;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.UserModels
{
    public class Guest : User, ISerializable
    {
        private double _rating;
        public double Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged();
                }
            }
        }
        public List<TourReservation> TourReservations { get; set; } = new();
        public List<AccommodationReservation> AccommodationReservations { get; set; } = new();
        public List<Voucher> Vouchers { get; set; } = new();

        public Guest()
        {
        }

        public Guest(int id, string username, string password, double rating = 0)
        {
            Id = id;
            Username = username;
            Password = password;
            _role = USER_ROLE.GUEST;
            Rating = rating;
            TourReservations = new List<TourReservation>();
            AccommodationReservations = new List<AccommodationReservation>();
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
                Role,
                Math.Round(Rating, 2).ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            Role = values[3];
            Rating = double.Parse(values[4]);
        }
    }
}