using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using SIMSProject.Domain.Models.UserModels;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models.TourModels
{
    public enum GuestAttendance { ABSENT = 0, PENDING, PRESENT }
    public class TourGuest: ISerializable
    {
        public int AppointmentId { get; set; }
        public int GuestId { get; set; }
        public int JoinedKeyPointId { get; set; }

        public GuestAttendance GuestStatus { get; set; }

        public Guest Guest { get; set; } = new(); 
        public KeyPoint JoinedKeyPoint { get; set; } = new();

        public TourGuest() { }

        public TourGuest(int tourDateId, int guestId, int keypoint)
        {
            AppointmentId = tourDateId;
            GuestId = guestId;
            JoinedKeyPointId = keypoint;
        }
        public override string ToString()
        {
            return $"{Guest.Username}, {GuestStatus}";
        }

        public string[] ToCSV()
        {
            string[] csvValues = { AppointmentId.ToString(), GuestId.ToString(), GuestStatus.ToString(), JoinedKeyPointId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AppointmentId = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            GuestStatus = (GuestAttendance)Enum.Parse(typeof(GuestAttendance), values[2]);
            JoinedKeyPointId = Convert.ToInt32(values[3]);
        }
    }
}
