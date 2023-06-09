using System;
using System.Globalization;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; } = new();
        public Guest1 Guest { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; } = 1;
        public int GuestNumber { get; set; } = 1;
        public bool GuestRated { get; set; } = false;
        public bool RateGuestNotificationSent { get; set; } = false;
        public bool OwnerRated { get; set; } = false;
        public bool Canceled { get; set; } = false;
        public string ReservationDetails { get; set; } = string.Empty;
        public double OwnerRating { get; set; } = 0;
        public double GuestRating { get; set; } = 0;
        public string OwnerRatingString { get => OwnerRated ? Math.Round(OwnerRating, 2).ToString() : "-"; }
        public string GuestRatingString { get => GuestRated ? Math.Round(GuestRating, 2).ToString() : "-"; }
        public bool IsInFuture { get => StartDate > DateTime.Now; }
        public string FormattedStartDate
        {
            get => StartDate.ToString("ddd, d. MMM. yyyy.");
        }
        public string FormattedEndDate
        {
            get => EndDate.ToString("ddd, d. MMM. yyyy.");
        }

        public AccommodationReservation()
        {
        }

        public AccommodationReservation(int accommodationId, int guestId, DateTime startDate, DateTime endDate,
            int numberOfDays, int guestsNumber, bool canceled)
        {
            Accommodation.Id = accommodationId;
            Guest.Id = guestId;
            StartDate = startDate;
            EndDate = endDate;
            NumberOfDays = numberOfDays;
            Canceled = canceled;
            GuestNumber = guestsNumber;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Accommodation.Id.ToString(),
                Guest.Id.ToString(),
                StartDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                EndDate.ToString(CultureInfo.GetCultureInfo("sr-LATN")),
                NumberOfDays.ToString(),
                GuestNumber.ToString(),
                GuestRated.ToString(),
                RateGuestNotificationSent.ToString(),
                OwnerRated.ToString(),
                Canceled.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Accommodation.Id = int.Parse(values[1]);
            Guest.Id = int.Parse(values[2]);
            StartDate = DateTime.Parse(values[3], CultureInfo.GetCultureInfo("sr-LATN"));
            EndDate = DateTime.Parse(values[4], CultureInfo.GetCultureInfo("sr-LATN"));
            NumberOfDays = int.Parse(values[5]);
            GuestNumber = int.Parse(values[6]);
            GuestRated = bool.Parse(values[7]);
            RateGuestNotificationSent = bool.Parse(values[8]);
            OwnerRated = bool.Parse(values[9]);
            Canceled = bool.Parse(values[10]);
        }

        public override string? ToString()
        {
            return $"Rezervacija (id: {Id}) " +
                   $"od <{StartDate:dd.MM.yy}> do <{EndDate:dd.MM.yy}> " +
                   $"za gosta <{Guest.Id}> (gost {(GuestRated ? "je" : "nije")} ocenjen)";
        }
    }
}