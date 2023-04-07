﻿using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models.TourModels
{
    public enum ObtainingReason { APPOINTMENTCANCELED = 0, GUIDEQUIT, WON }

    public class Voucher: ISerializable
    {
        public int Id;
        public int GuestId;
        public ObtainingReason Reason;
        public DateTime Expiration;

        public Voucher(int guestId, ObtainingReason reason)
        {
            GuestId = guestId;
            Reason = reason;
            Expiration = CalculateExpirationDate(reason);
        }

        public Voucher()
        {

        }

        private static DateTime CalculateExpirationDate(ObtainingReason reason)
        {
            switch (reason)
            {
                case ObtainingReason.APPOINTMENTCANCELED: return DateTime.Now.AddYears(1);
                case ObtainingReason.GUIDEQUIT: return DateTime.Now.AddYears(2);
                case ObtainingReason.WON: return DateTime.Now.AddMonths(6);
                default:
                    break;
            }
            throw new ArgumentException("Reason not acceptable.");
        }

        public string FormattedDate => $"{Expiration:dd/MM/yyyy.}";
        public override string ToString()
        {
            return $"{Reason}, {FormattedDate}";
        }

        public string[] ToCSV()
        {
            throw new NotImplementedException();
        }

        public void FromCSV(string[] values)
        {
            throw new NotImplementedException();
        }
    }
}
