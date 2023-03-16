using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

public enum Status { ABSENT = 0, PENDING, PRESENT};

namespace SIMSProject.Model
{
    public class TourGuest : ISerializable
    {
        public int TourId { get; set; }
        public int GuestId { get; set; }

        private Status _guestStatus;
        public string GuestStatus
        {
            get
            {
                return _guestStatus switch
                {
                    Status.PRESENT => "Prisutan",
                    Status.ABSENT => "Odsutan",
                    _ => "Prijavljen"
                };
            }
            set
            {
                _guestStatus = value switch
                {
                    "Prisutan" => Status.PRESENT,
                    "Odsutan" => Status.ABSENT,
                    _ => Status.PENDING
                };
            }
        }
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
            string[] csvValues = {TourId.ToString(), GuestId.ToString(), GuestStatus};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
        
            TourId = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            GuestStatus = values[2];        
        }
    }
}
