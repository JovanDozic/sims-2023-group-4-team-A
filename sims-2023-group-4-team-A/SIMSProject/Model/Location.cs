using SIMSProject.Serializer;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace SIMSProject.Model
{
    public class Location : ISerializable,  IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string _city = string.Empty;
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

        private string _country = string.Empty;
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

        public Location()
        {
            
        }

        public Location(int id, string city, string country)
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

        public bool IsValid
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
