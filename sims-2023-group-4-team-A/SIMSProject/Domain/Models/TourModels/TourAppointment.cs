using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace SIMSProject.Domain.Models.TourModels
{
    public enum Status { ACTIVE = 0, INACTIVE, COMPLETED, CANCELED }
    public class TourAppointment
    {
        public int Id { get; set; }
        public int CurrentKeyPointId { get; set; }
        public int TourId { get; set; }
        public DateTime Date { get; set; }
        public Status TourStatus { get; set; }
        public int AvailableSpots { get; set; }
        public Tour Tour { get; set; } = new();
        public KeyPoint CurrentKeyPoint { get; set; } = new();
        public List<Guest> Guests { get; set; } = new();
        public TourAppointment() { }

        public TourAppointment(DateTime date, int tourId, int availableSpots, int currentKeyPointId)
        {
            if (IsPastToday(date))
            {
                throw new ArgumentException("Error! Invalid date.");
            }

            Date = date;
            TourId = tourId;
            AvailableSpots = availableSpots;
            CurrentKeyPointId = currentKeyPointId;
        }

        private static bool IsPastToday(DateTime date)
        {
            return DateTime.Compare(DateTime.Now, date) > 0 ;
        }

        public override string ToString()
        {
            return $"{Date}, {TourStatus}";
        }
    }
}
