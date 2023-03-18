using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model.UserModel
{
    public class Owner : User, ISerializable
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
        
        // TODO: Add list of owned accommodations

        public Owner() { }

        public Owner(int id, string username, string password, double rating = 0)
        {
            Id = id;
            Username = username;
            Password = password;
            _role = USER_ROLE.OWNER;
            Rating = rating;
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
