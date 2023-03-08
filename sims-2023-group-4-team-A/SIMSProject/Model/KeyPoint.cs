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
        public int TourId { get; set; }

        public KeyPoint()
        {
            
        }
        public KeyPoint(int id, string description, int tourId)
        {
            Id = id;
            Description = description;
            TourId = tourId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Description, TourId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Description = values[1];
            Id = Convert.ToInt32(values[2]);
        }
    }
}
