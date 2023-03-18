using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SIMSProject.Model.UserModel
{
    public class Guest : User, ISerializable
    {
        private double _rating = 0;
        public double Rating
        {
            get => _rating;
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }
        public List<TourReservation> TourReservations { get; set; } = new();
        public List<AccommodationReservation> AccommodationReservations { get; set; } = new();

        public Guest() { }

        public Guest(int id, string username, string password, USER_ROLE role, double rating)
        {
            Id = id;
            Username = username;
            Password = password;
            _role = USER_ROLE.GUEST;
            Rating = rating;
            TourReservations = new();
            AccommodationReservations = new();
            // TODO: load tours and  reservations
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Username,
                Password,
                Role,
                Math.Round(Rating, 2).ToString(),
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
