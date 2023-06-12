
using System;
using System.Globalization;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    public class ReschedulingRequest : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; } = new();
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public string OwnerComment { get; set; } = string.Empty;
        public ReschedulingRequestStatus Status { get; set; } = ReschedulingRequestStatus.None;
        public string RequestDetails { get; set; } = string.Empty;
        public string FormattedNewStartDate
        {
            get => NewStartDate.ToString("ddd, d. MMM. yyyy.");
        }
        public string FormattedNewEndDate
        {
            get => NewEndDate.ToString("ddd, d. MMM. yyyy.");
        }

        public static ReschedulingRequestStatus GetStatus(string status)
        {
            return status switch
            {
                "Na čekanju" => ReschedulingRequestStatus.Waiting,
                "Odobren" => ReschedulingRequestStatus.Accepted,
                "Odbijen" => ReschedulingRequestStatus.Rejected,
                _ => ReschedulingRequestStatus.None,
            };
        }

        public static string GetStatus(ReschedulingRequestStatus status)
        {
            return status switch
            {
                ReschedulingRequestStatus.Waiting => "Na čekanju",
                ReschedulingRequestStatus.Accepted => "Odobren",
                ReschedulingRequestStatus.Rejected => "Odbijen",
                _ => string.Empty
            };
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Reservation.Id.ToString(),
                NewStartDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                NewEndDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                OwnerComment,
                GetStatus(Status)
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            var i = 0;
            Id = int.Parse(values[i++]);
            Reservation.Id = int.Parse(values[i++]);
            NewStartDate = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
            NewEndDate = DateTime.Parse(values[i++], CultureInfo.GetCultureInfo("sr-LATN"));
            OwnerComment = values[i++];
            Status = GetStatus(values[i++]);
        }
    }
}
