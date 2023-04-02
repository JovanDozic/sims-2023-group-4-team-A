using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SIMSProject.Model.UserModel;
using System.Threading.Tasks;



namespace SIMSProject.Model
{
    public enum GuestAttendance { ABSENT = 0, PENDING, PRESENT }
    public class TourGuest : ISerializable
    {
        public int TourDateId { get; set; }
        public int GuestId { get; set; }
        public int JoinedKeyPointId { get; set; }

        private GuestAttendance _guestStatus;
        public string GuestStatus
        {
            get
            {
                return _guestStatus switch
                {
                    GuestAttendance.PRESENT => "Prisutan",
                    GuestAttendance.ABSENT => "Odsutan",
                    _ => "Prijavljen"
                };
            }
            set
            {
                _guestStatus = value switch
                {
                    "Prisutan" => GuestAttendance.PRESENT,
                    "Odsutan" => GuestAttendance.ABSENT,
                    _ => GuestAttendance.PENDING
                };
            }
        }

        //public User Guest { get; set; }

        public TourDate TourDate { get; set; } = new();
        public KeyPoint JoinedKeyPoint { get; set; } = new();

        public TourGuest() { }

        public TourGuest(int tourDateId, int guestId, int keypoint)
        {
            TourDateId = tourDateId;
            GuestId = guestId;
            JoinedKeyPointId = keypoint;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {TourDateId.ToString(), GuestId.ToString(), GuestStatus, JoinedKeyPointId .ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
        
            TourDateId = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            GuestStatus = values[2];
            JoinedKeyPointId = Convert.ToInt32(values[3]);
        }
    }
}
