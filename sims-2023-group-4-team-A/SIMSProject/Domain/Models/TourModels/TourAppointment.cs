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
        public DateTime AppointmentsEnd { get => this.Date.AddHours(this.Tour.Duration); }
        public Status TourStatus { get; set; } = Status.INACTIVE;
        public int AvailableSpots { get; set; }
        public Tour Tour { get; set; } = new();
        public KeyPoint CurrentKeyPoint { get; set; } = new();
        public Guide Guide { get; set; } = new();
        public TourAppointment() { }

        public TourAppointment(DateTime date, int tourId, int availableSpots, int currentKeyPointId, int guideId)
        {
            if (IsPastToday(date))
            {
                throw new ArgumentException("Error! Invalid date.");
            }

            Guide.Id = guideId;
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
            {
                Id.ToString(),
                Date.ToString("dd.MM.yyyy HH:mm:ss"),
                Tour.Id.ToString(),
                Guide.Id.ToString(),
                TourStatus.ToString(),
                AvailableSpots.ToString(),
                CurrentKeyPoint.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Date = DateTime.Parse(values[1]);
            Tour.Id = Convert.ToInt32(values[2]);
            Guide.Id = Convert.ToInt32(values[3]);
            TourStatus = (Status)Enum.Parse(typeof(Status), values[4]);
            AvailableSpots = Convert.ToInt32(values[5]);
            CurrentKeyPoint.Id = Convert.ToInt32(values[6]);
        }
    }
}
