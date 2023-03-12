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
using System.Runtime.CompilerServices;

namespace SIMSProject.Model
{
    public class TourLocation : ISerializable,  IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _city;
        public string City 
        {   get => _city;
            set 
            { 
                if(value != _city)
                {
                    _city = value;
                    OnPropertyChanged(nameof(City));
                    
                }
            }
        }

        private string _country;
        public string Country 
        { 
            get => _country;
            set
            {
                if(value != _country)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            } 
        }




        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return $"{City}, {Country}";
        }

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
                        return "Država";

                    if (!Regex.IsMatch(Country, templateRegex))
                        return "Ne moze biti država";
                }
                else if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        return "Grad";
                    if (!Regex.IsMatch(City, templateRegex))
                        return "Ne moze biti grad";
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
    }

    
}
