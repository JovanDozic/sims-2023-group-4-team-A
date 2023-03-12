using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.Model
{
    public class TourLocation : ISerializable
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        public TourLocation()
        {
            
        }

        public TourLocation(int id, string city, string country)
        {
            Id = id;
            City = city;
            Country = country;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {Id.ToString(), City.ToString(), Country.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            City = Convert.ToString(values[1]);
            Country = Convert.ToString(values[2]);
        }

        public override string? ToString()
        {
            return City + " " + Country;
        }
    }

    
}
