using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models.TourModels
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int GuestNumber { get; set; }
        public bool GuideRated { get; set; } = false;
        public TourAppointment TourAppointment { get; set; } = new();
        public TourReservation() { }

        public TourReservation(int id, int tourDateId, int guestId, int guestNumber)
        {
            Id = id;
            TourAppointment.Id = tourDateId;
            GuestId = guestId;
            GuestNumber = guestNumber;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), TourAppointment.Id.ToString(), GuestId.ToString(), GuestNumber.ToString(), GuideRated.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] value)
        {
            Id = Convert.ToInt32(value[0]);
            TourAppointment.Id = Convert.ToInt32(value[1]);
            GuestId = Convert.ToInt32(value[2]);
            GuestNumber = Convert.ToInt32(value[3]);
            GuideRated = bool.Parse(value[4]);
        }
    }
}
