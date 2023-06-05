using Dynamitey.DynamicObjects;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;

namespace SIMSProject.Domain.Models.TourModels
{
    public enum ComplexRequestStatus { ONHOLD = 0, INVALID, ACCEPTED };
    public class ComplexTourRequest : ISerializable
    {
        public int Id { get; set; }
        public Guest2 Guest { get; set; } = new();
        public string Name { get; set; } = string.Empty;
        public List<CustomTourRequest> Parts { get; set; } = new();
        public RequestStatus Status { get; set;}

        public ComplexTourRequest() { }

        public ComplexTourRequest(int id, int guestId, string name, RequestStatus status)
        {
            Id = id;
            Guest.Id = guestId;
            Name = name;
            Status = status;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Guest.Id.ToString(),
                Name.ToString(),
                Status.ToString()
            };
            return csvValues;
        }
        
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Guest.Id = Convert.ToInt32(values[1]);
            Name = Convert.ToString(values[2]);
            Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), values[3]); 
        }
    }
}
