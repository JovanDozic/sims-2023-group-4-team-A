using System;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;

namespace SIMSProject.Domain.Models.AccommodationModels
{
    public class AccommodationReservation : ISerializable
    {
        public int Id { get; set; }
        public Accommodation Accommodation { get; set; } = new();
        public Guest Guest { get; set; } = new();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public int GuestNumber { get; set; }
        public bool GuestRated { get; set; }
        public bool OwnerRated { get; set; }
        public bool Canceled { get; set; } = false;

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
                StartDate.ToString(),
                EndDate.ToString(),
                NumberOfDays.ToString(),
                GuestNumber.ToString(),
                GuestRated.ToString(),
                OwnerRated.ToString(),
                Canceled.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Accommodation.Id = int.Parse(values[1]);
            Guest.Id = int.Parse(values[2]);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
            NumberOfDays = int.Parse(values[5]);
            GuestNumber = int.Parse(values[6]);
            GuestRated = bool.Parse(values[7]);
            OwnerRated = bool.Parse(values[8]);
            Canceled = bool.Parse(values[9]);
        }
    }
}