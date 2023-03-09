using SIMSProject.Model;
using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model
{
    public class TourDate : ISerializable
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int TourId { get; set; }

        public TourDate()
        {
            
        }

        public TourDate(int id, DateTime dateTime, int tourId)
        {
            Id = id;
            DateTime = dateTime;
            TourId = tourId;
        }

            public void FromCSV(string[] values)
            {
                Id = Convert.ToInt32(values[0]);
                DateTime = DateTime.Parse(values[1]);
                TourId = Convert.ToInt32(values[2]);
            }

            public string[] ToCSV()
            {
                string[] csvValues = { Id.ToString(), DateTime.ToString(), TourId.ToString() };
                return csvValues;
            }
    }
}
