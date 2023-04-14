using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.Domain.Models.TourModels
{
    public class KeyPoint
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public Location Location { get; set; } = new();
        public List<Tour> Tours { get; set; } = new List<Tour>();
        public KeyPoint()
        {

        }
        public KeyPoint(int id, string description, Location location, int locationId)
        {
            Id = id;
            Description = description;
            Location = location;
            LocationId = locationId;
        }

        public override string ToString()
        {
            return $"{Description} {Location}";
        }
    }
}
