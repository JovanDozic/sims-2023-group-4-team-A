using System;
using System.Collections.Generic;
using SIMSProject.Serializer;

namespace SIMSProject.Model.UserModel
{
    public class Guide : User, ISerializable
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

        public List<Tour> Tours { get; set; } = new();
        public List<TourReservation> TourReservations { get; set; } = new();
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
            _role = USER_ROLE.GUIDE;
            Rating = rating;
        }

        public override string ToString()
        {
            return $"{Username}";
        }

        public new string[] ToCSV()
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

        public new void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            Role = values[3];
            Rating = double.Parse(values[4]);
        }
    }
}