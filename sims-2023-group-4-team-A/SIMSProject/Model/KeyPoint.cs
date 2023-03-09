using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model
{
    public class KeyPoint : ISerializable
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;

        public Address Location { get; set; }

        public List<Tour> Tours { get; set; } =  new List<Tour>();

        public int LocationId { get; set; }

        public KeyPoint()
        {
            
        }
        public KeyPoint(int id, string description, Address location, List<Tour> tours, int locationId)
        {
            Id = id;
            Description = description;
            Location = location;
            Tours = tours;
            LocationId = locationId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Description, LocationId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Description = values[1];
            LocationId = Convert.ToInt32(values[2]);
        }
    }
}
