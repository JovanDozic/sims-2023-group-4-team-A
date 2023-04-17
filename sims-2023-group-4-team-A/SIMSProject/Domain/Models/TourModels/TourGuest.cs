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
        public TourAppointment TourAppointment { get; set; } = new();
        public GuestAttendance GuestStatus { get; set; }
        public Guest Guest { get; set; } = new(); 
        public KeyPoint JoiningPoint { get; set; } = new();

        public TourGuest() { }

        public TourGuest(int tourDateId, int guestId, int keypoint)
        {
            TourAppointment.Id = tourDateId;
            Guest.Id = guestId;
            JoiningPoint.Id = keypoint;
        }
        public override string ToString()
        {
            return $"{Guest.Username}, {GuestStatus}";
        }

        public string[] ToCSV()
        {
            string[] csvValues = { TourAppointment.Id.ToString(), Guest.Id.ToString(), GuestStatus.ToString(), JoiningPoint.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            TourAppointment.Id = Convert.ToInt32(values[0]);
            Guest.Id = Convert.ToInt32(values[1]);
            GuestStatus = (GuestAttendance)Enum.Parse(typeof(GuestAttendance), values[2]);
            JoiningPoint.Id = Convert.ToInt32(values[3]);
        }
    }
}
