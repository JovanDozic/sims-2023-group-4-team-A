using SIMSProject.Controller;
using SIMSProject.Serializer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace SIMSProject.Model
{
    public enum ACCOMMODATION_TYPE { APARTMENT = 0, HOUSE, HUT};
    public class Accommodation : ISerializable, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }
        private Address _location = new();
        public Address Location
        {
            get => _location;
            set
            {
                if (value != _location)
                {
                    _location = value;
                    OnPropertyChanged(nameof(Location));
                }
            }
        }
        private ACCOMMODATION_TYPE _type;
        public ACCOMMODATION_TYPE Type
        {
            get => _type;
            set
            {
                if (value != _type)
                {
                    _type = value;
                    OnPropertyChanged(nameof(Type));
                }
            }
        }
        private int _maxGuestNumber = 1;
        public int MaxGuestNumber
        {
            get => _maxGuestNumber;
            set
            {
                if (value != _maxGuestNumber && value >= 1)
                {
                    _maxGuestNumber = value;
                    OnPropertyChanged(nameof(MaxGuestNumber));
                }
            }
        }
        private int _minReservationDays = 1;
        public int MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays && value >= 1)
                {
                    _minReservationDays = value;
                    OnPropertyChanged(nameof(MinReservationDays));
                }
            }
        }
        private int _cancellationThreshold = 1;
        public int CancellationThreshold
        {
            get => _cancellationThreshold;
            set
            {
                if (value != _cancellationThreshold && value >= 1)
                {
                    _cancellationThreshold = value;
                    OnPropertyChanged(nameof(CancellationThreshold));
                }
            }
        }
        public List<string> ImageURLs { get; set; }

        public Accommodation()
        {
            Location = new();
            ImageURLs = new();
        }

        public Accommodation(string name, Address location, ACCOMMODATION_TYPE type, int maxGuestNumber, int minReservationDays, int cancellationThreshold, List<string> imageURLs)
        {
            Name = name;
            Location = location;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            CancellationThreshold = cancellationThreshold;
            ImageURLs = new();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ACCOMMODATION_TYPE GetType(string type)
        {
            if (type == "APARTMENT") return ACCOMMODATION_TYPE.APARTMENT;
            else if (type == "HOUSE") return ACCOMMODATION_TYPE.HOUSE;
            else return ACCOMMODATION_TYPE.HUT;
        }

        public string[] ToCSV()
        {
            string imageURLs = string.Empty;
            if (ImageURLs.Count > 0)
            {
                foreach (var imageURL in ImageURLs) imageURLs += imageURL + ",";
                imageURLs = imageURLs.Remove(imageURLs.Length - 1);
            }
            string[] csvValues = { 
                Id.ToString(), 
                Name, 
                Location.Id.ToString(), 
                Type.ToString(), 
                MaxGuestNumber.ToString(), 
                MinReservationDays.ToString(), 
                CancellationThreshold.ToString(),
                imageURLs
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            AddressController addressController = new();
            Location = addressController.GetById(int.Parse(values[2]));
            Type = GetType(values[3]);
            MaxGuestNumber = int.Parse(values[4]);
            MinReservationDays = int.Parse(values[5]);
            CancellationThreshold = int.Parse(values[6]);
            var imageURLs = values[7].Split(',');
            foreach (var imageURL in imageURLs) if (imageURL != string.Empty) ImageURLs.Add(imageURL);

            Trace.Write("\nAccommodation [" + Id + "] has " + ImageURLs.Count + " images: ");
            foreach (var imageURL in ImageURLs) Trace.Write(imageURL + "; ");
        }
    }
}
