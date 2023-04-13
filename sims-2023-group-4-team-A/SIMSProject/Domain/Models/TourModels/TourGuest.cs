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
    public class TourGuest
    {
        public int AppointmentId { get; set; }
        public int GuestId { get; set; }
        public int JoinedKeyPointId { get; set; }

        public GuestAttendance GuestStatus { get; set; }

        public Guest Guest { get; set; } = new(); 
        public TourAppointment Appointment { get; set; } = new();
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
    }
}
