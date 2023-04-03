using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    public class CancelledReservationsNotifications: ISerializable
    {
        public int Id { get; set; }
        public String Message { get; set; }
        public bool Read { get; set; }

        public CancelledReservationsNotifications(string message, bool read)
        {
            Message = message;
            Read = read;
        }

        public CancelledReservationsNotifications()
        {
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Message,
                Read.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Message = values[1];
            Read = bool.Parse(values[2]);
        }
    }
}
