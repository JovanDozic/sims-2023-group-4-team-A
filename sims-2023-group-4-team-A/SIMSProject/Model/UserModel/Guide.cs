using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;



namespace SIMSProject.Model.UserModel
{
    public enum Title { REGULAR = 0, SUPERGUIDE }
    public class Guide: User, ISerializable
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

        private Title _guideTitle;
        public string GuideTitle
        {
            get => _guideTitle switch
            {
                Title.REGULAR => "Običan",
                _ => "Super vodič"
            };
            set => _guideTitle = value switch
            {
                "Običan" => Title.REGULAR,
                _ => Title.SUPERGUIDE
            };
        }

        public List<Tour> Tours { get; set; } = new();

        public Guide()
        {
            
        }

        public Guide(int id, string username, string password,  double rating)
        {
            Id = id;
            Username = username;
            Password = password;
            _role = USER_ROLE.GUIDE;
            Rating = rating;
            GuideTitle = "Običan";
            _role = USER_ROLE.GUIDE;
        }

        public string[] ToCSV()
        {
            string[] CSVValue = {Id.ToString(), Username, Password, Rating.ToString(), Role};
            return CSVValue;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Rating = Convert.ToDouble(values[3]);
            Role = values[4];
        }
    }
}
