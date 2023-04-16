using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace SIMSProject.Domain.Models.TourModels
{
    public enum Status { ACTIVE = 0, INACTIVE, COMPLETED, CANCELED }
    public class TourAppointment : ISerializable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Status TourStatus { get; set; } = Status.INACTIVE;
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
            Tour.Id = tourId;
            AvailableSpots = availableSpots;
            CurrentKeyPoint.Id = currentKeyPointId;
        }

        private static bool IsPastToday(DateTime date)
        {
            return DateTime.Compare(DateTime.Now, date) > 0;
        }

        public override string ToString()
        {
            return $"{Date}, {TourStatus}";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
                { Id.ToString(),
                Date.ToString("dd.MM.yyyy HH:mm:ss"),
                Tour.Id.ToString(),
                TourStatus.ToString(),
                AvailableSpots.ToString(),
                CurrentKeyPoint.Id.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Date = DateTime.Parse(values[1]);
            Tour.Id = Convert.ToInt32(values[2]);
            TourStatus = (Status)Enum.Parse(typeof(Status), values[3]);
            AvailableSpots = Convert.ToInt32(values[4]);
            CurrentKeyPoint.Id = Convert.ToInt32(values[5]);
        }
    }
}
