using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int TourDateId { get; set; }
        public int GuestNumber { get; set; }

        public TourReservation() { }

        public TourReservation(int id, int guestNumber, int tourDateId, int guestId)
        {
            Id = id;
            GuestNumber = guestNumber;
            TourDateId = tourDateId;
            GuestId = guestId;
        }   

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), GuestNumber.ToString(),TourDateId.ToString(), GuestId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] value)
        {
            Id = Convert.ToInt32(value[0]);
            GuestNumber = Convert.ToInt32(value[1]);
            TourDateId = Convert.ToInt32(value[2]);
            GuestId = Convert.ToInt32(value[3]);
        }
    }
}
