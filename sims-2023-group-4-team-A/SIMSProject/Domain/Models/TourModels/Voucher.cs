﻿using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models.TourModels
{
    public enum ObtainingReason { APPOINTMENTCANCELED = 0, GUIDEQUIT, WON }

    public class Voucher : ISerializable
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public Guest2 Guest { get; set; } = new();
        public ObtainingReason Reason { get; set; }
        public DateTime Expiration { get; set; }
        public bool Used { get; set; } = false;

        public Voucher(int guestId, int guideId, ObtainingReason reason, bool used)
        {
            Guest.Id = guestId;
            GuideId = guideId;
            Reason = reason;
            Expiration = CalculateExpirationDate(reason);
            Used = used;
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
            string[] csvValues = { Id.ToString(), GuideId.ToString(),Guest.Id.ToString(), Reason.ToString(), Expiration.ToString(), Used.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            GuideId = Convert.ToInt32(values[1]);
            Guest.Id = Convert.ToInt32(values[2]);
            Reason = (ObtainingReason)Enum.Parse(typeof(ObtainingReason), values[3]);
            Expiration = DateTime.Parse(values[4]);
            Used = bool.Parse(values[5]);
        }
    }
}
