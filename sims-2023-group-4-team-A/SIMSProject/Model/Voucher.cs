using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum ObtainingReason { APPOINTMENTCANCELED = 0, GUIDEQUIT, WON }

namespace SIMSProject.Model
{
    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        private int _guestId;
        public int GuestId
        { 
            get => _guestId; 
            set => _guestId = value;
        }
        private ObtainingReason _reason;
        public string Reason
        {
            get => _reason switch
            {
                ObtainingReason.APPOINTMENTCANCELED => "Termin otkazan",
                ObtainingReason.GUIDEQUIT => "Vodič dao otkaz",
                _ => "Osvojen"
            };
            set => _reason = value switch
            { 
                "Termin otkazan" => ObtainingReason.APPOINTMENTCANCELED,
                "Vodič dao otkaz" => ObtainingReason.GUIDEQUIT,
                _ => ObtainingReason.WON
            };
        }

 
        private DateTime _expiration;
        public DateTime Expiration { get; set; }
        public string FormattedDate => $"{Expiration:dd/MM/yyyy.}";
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), 
                GuestId.ToString(), 
                Reason.ToString(), 
                Expiration.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuestId = Convert.ToInt32(values[1]);
            Reason = Convert.ToString(values[2]);
            Expiration = Convert.ToDateTime(values[3]);
        }

        public override string ToString()
        {
            return $"{Reason}, {FormattedDate}";
        }

        
    }
}
