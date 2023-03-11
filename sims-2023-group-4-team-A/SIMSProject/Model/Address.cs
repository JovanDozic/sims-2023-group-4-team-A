using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using SIMSProject.Serializer;

namespace SIMSProject.Model
{
    public class Address : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        private string _street = string.Empty;
        public string Street
        {
            get => _street;
            set
            {
                if (_street != value)
                {
                    _street = value;
                    OnPropertyChanged(nameof(Street));
                }
            }
        }
        private string _streetNumber = string.Empty;
        public string StreetNumber
        {
            get => _streetNumber;
            set
            {
                if (_streetNumber != value)
                {
                    _streetNumber = value;
                    OnPropertyChanged(nameof(StreetNumber));
                }
            }
        }
        private string _city = string.Empty;
        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
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
                if (_country != value)
                {
                    _country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        public Address()
        {
        }

        public Address(int id, string street, string streetNumber, string city, string country)
        {
            Id = id;
            Street = street;
            StreetNumber = streetNumber;
            City = city;
            Country = country;
        }



        // [SERIALIZATION HANDLING]

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Street, StreetNumber, City, Country };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Street = values[1];
            StreetNumber = values[2];
            City = values[3];
            Country = values[4];
        }



        // [VALIDATION HANDLING]
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Street" && string.IsNullOrEmpty(Street)) return "Ulica je obavezna.";
                else if (columnName == "StreetNumber" && string.IsNullOrEmpty(StreetNumber)) return "Broj je obavezan.";
                else if (columnName == "City" && string.IsNullOrEmpty(City)) return "Grad je obavezan.";
                else if (columnName == "Country" && string.IsNullOrEmpty(Country)) return "Država je obavezna.";
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Street", "StreetNumber", "City", "Country" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties) if (this[property] != null) return false;
                return true;
            }
        }



        // [PROPERTY CHANGED EVENT HANDLER]

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
