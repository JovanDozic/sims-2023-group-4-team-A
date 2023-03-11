using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model
{
    public class TourGuest : ISerializable
    {
        public int TourId { get; set; }
        public int GuestId { get; set; }

        public TourGuest()
        {
            
        }

        public TourGuest(int tourId, int guestId)
        {
            TourId = tourId;
            GuestId = guestId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {TourId.ToString(), GuestId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
           TourId = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
        }
    }
}
