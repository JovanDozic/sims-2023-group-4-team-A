using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SIMSProject.Model.UserModel;
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

        public Owner()
        {

        }

        public Owner(int id, string username, string password, int rating = 0)
        {
            Id = id;
            Username = username;
            Password = password;
            _role = USER_ROLE.OWNER;
            Rating = rating;
            // TODO: Add list of Accommodation IDs that Owner owns
        }

        public new string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role, Math.Round(Rating, 2).ToString() };
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
