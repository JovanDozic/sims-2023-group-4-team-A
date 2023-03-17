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
        public List<TourReservation> TourReservations { get; set; } = new();
        public Guest() { }
        public Guest(int id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
            _role = USER_ROLE.OWNER;
        }
        public new string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role};
            return csvValues;
        }
        public new void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            Role = values[3];
        }
    }
}
