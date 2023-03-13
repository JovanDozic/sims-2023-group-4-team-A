using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace SIMSProject.Model
{
    public class TourLocation : ISerializable,  IDataErrorInfo
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


        /*Validation*/

        string templateRegex = "^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$";

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Country")
                {
                    if (string.IsNullOrEmpty(Country))
                        return "Grad";

                    if (!Regex.IsMatch(Country, templateRegex))
                        return "Ne moze biti grad";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "Drzava";
                    if (!Regex.IsMatch(City, templateRegex))
                        return "Ne moze biti drzava";
                }
               

                return null;
            }
        }

        private readonly string[] _validatesProperties = { "City", "Country"};

        public bool isValid
        {
            get
            {
                foreach (var property in _validatesProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
            
        public override string? ToString()
        {
            return City + " " + Country;
        }
    }

    
}
