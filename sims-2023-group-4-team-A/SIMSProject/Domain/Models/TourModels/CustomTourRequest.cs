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
        public Guest2 Guest { get; set; } = new();
        public Location Location { get; set; } = new();
        public string Description { get; set; } = string.Empty;
        public Language TourLanguage { get; set; } = Language.ENGLISH;
        public int GuestCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestCreateDate { get; set; }
        public RequestStatus RequestStatus { get; set; }

        public CustomTourRequest() { }
        public CustomTourRequest(int id, int guestId, int locationId, string description, Language tourLanguage, int guestCount, DateTime startDate, DateTime endDate, DateTime requestCreateDate, RequestStatus requestStatus)
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
        public string FormattedDateRange => $"{StartDate:dd/MM/yyyy}" + "-" + $"{EndDate:dd/MM/yyyy.}";
        public static RequestStatus GetStatus(string status)
        {
            return status switch
            {
                "Na čekanju" => RequestStatus.ONHOLD,
                "Nevažeći" => RequestStatus.INVALID,
                _ => RequestStatus.ACCEPTED
            };
        }

        public static string GetStatus(RequestStatus status)
        {
            return status switch
            {
                RequestStatus.ONHOLD => "Na čekanju",
                RequestStatus.INVALID => "Nevažeći",
                _ => "Prihvaćen"
            };
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Description,
                TourLanguage.ToString(),
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
            TourLanguage = (Language)Enum.Parse(typeof(Language), values[2]);
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
