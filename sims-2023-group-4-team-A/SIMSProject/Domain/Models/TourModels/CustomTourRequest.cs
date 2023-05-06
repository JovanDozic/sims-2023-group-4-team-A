using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Domain.Models.TourModels
{
    public enum RequestStatus { ONHOLD = 0, INVALID, ACCEPTED };
    public class CustomTourRequest : ISerializable
    {
        public int Id { get; set; }
        public Guest Guest { get; set; } = new();
        public Location Location { get; set; } = new();
        public string Description { get; set; } = string.Empty; 
        public string TourLanguage { get; set; }
        public int GuestCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestCreateDate { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public CustomTourRequest() { }
        public CustomTourRequest(int id, int guestId, int locationId, string description, string tourLanguage, int guestCount, DateTime startDate, DateTime endDate, DateTime requestCreateDate, RequestStatus requestStatus)
        {
            Id = id;
            Guest.Id = guestId;
            Location.Id = locationId;
            Description = description;
            TourLanguage = tourLanguage;
            GuestCount = guestCount;
            StartDate = startDate;
            EndDate = endDate;
            RequestCreateDate = requestCreateDate;
            RequestStatus = requestStatus;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Description,
                TourLanguage,
                GuestCount.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                RequestCreateDate.ToString(),
                RequestStatus.ToString(),
                Guest.Id.ToString(),
                Location.Id.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Description = values[1];
            TourLanguage = values[2];
            GuestCount = Convert.ToInt32(values[3]);
            StartDate = DateTime.Parse(values[4]);
            EndDate = DateTime.Parse(values[5]);
            RequestCreateDate = DateTime.Parse(values[6]);
            RequestStatus = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[7]);
            Guest.Id = Convert.ToInt32(values[8]);
            Location.Id = Convert.ToInt32(values[9]);
        }
    }
}
