using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SIMSProject.Model
{
    public enum ObtainingReason { APPOINTMENTCANCELED = 0, GUIDEQUIT, WON }

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
                ObtainingReason.WON => "Osvojen",
                _ => throw new NotImplementedException()
            };
            set => _reason = value switch
            {
                "Termin otkazan" => ObtainingReason.APPOINTMENTCANCELED,
                "Vodič dao otkaz" => ObtainingReason.GUIDEQUIT,
                "Osvojen" => ObtainingReason.WON,
                _ => throw new NotImplementedException()
            };
        }


        private DateTime _expiration;
        public DateTime Expiration
        {
            get => _expiration;
            set => _expiration = value;
        }

        public Voucher(int guestId, string reason)
        {
            GuestId = guestId;
            Reason = reason;
            Expiration = CalculateExpirationDate(reason);
        }

        public Voucher()
        {
            
        }

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

        private static DateTime CalculateExpirationDate(string reason)
        {
            switch (reason)
            {
                case "Termin otkazan": return DateTime.Now.AddYears(1);
                case "Vodič dao otkaz": return DateTime.Now.AddYears(2);
                case "Osvojen": return DateTime.Now.AddMonths(6);
                default: 
                    break;
            }
            throw new ArgumentException("Reason not acceptable.");
        }
    }
}
