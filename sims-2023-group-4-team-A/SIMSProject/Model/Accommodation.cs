using SIMSProject.Controller;
using SIMSProject.Model.UserModel;
using SIMSProject.Serializer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Documents.Serialization;

namespace SIMSProject.Model
{
    public enum ACCOMMODATION_TYPE { APARTMENT = 0, HOUSE, HUT };
    public class Accommodation : ISerializable, INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }
        public Owner Owner { get; set; } = new();
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
        private Location _location = new();
        public Location Location
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
        public string Type
        {
            get
            {
                return _type switch
                {
                    ACCOMMODATION_TYPE.APARTMENT => "Apartman",
                    ACCOMMODATION_TYPE.HOUSE => "Kuća",
                    _ => "Koliba"
                };
            }
            set
            {
                _type = value switch
                {
                    "Apartman" => ACCOMMODATION_TYPE.APARTMENT,
                    "Kuća" => ACCOMMODATION_TYPE.HOUSE,
                    _ => ACCOMMODATION_TYPE.HUT
                };
                OnPropertyChanged(nameof(Type));
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
        private List<string> _imageURLs = new();
        public List<string> ImageURLs
        {
            get => _imageURLs;
            set
            {
                if (_imageURLs != value)
                {
                    _imageURLs = value;
                    OnPropertyChanged(nameof(ImageURLs));
                }
            }
        }
        private string _imageURLsCSV = string.Empty;
        public string ImageURLsCSV
        {
            get => _imageURLsCSV;
            set
            {
                if (_imageURLsCSV != value)
                {
                    _imageURLsCSV = value.Replace(" ", "").Replace("\n", " ").Replace("\t", "");
                    OnPropertyChanged(nameof(ImageURLsCSV));
                }
            }
        }
        private List<AccommodationReservation> _reservations = new();
        public List<AccommodationReservation> Reservations
        {
            get => _reservations;
            set
            {
                if (value != _reservations)
                {
                    _reservations = value;
                    OnPropertyChanged(nameof(Reservations));
                }
            }
        }

        public Accommodation() { }

        public Accommodation(int ownerId, string name, Location location, string type, int maxGuestNumber, int minReservationDays, int cancellationThreshold, string imageURLsCSV)
        {
            Owner.Id = ownerId;
            Name = name;
            Location = location;
            Type = type;
            MaxGuestNumber = maxGuestNumber;
            MinReservationDays = minReservationDays;
            CancellationThreshold = cancellationThreshold;
            ImageURLs = new();
            ImageURLsCSV = imageURLsCSV;
            ImageURLsFromCSV(ImageURLsCSV);
        }

        public string[] ToCSV()
        {
            ImageURLsToCSV();
            string[] csvValues = {
                Id.ToString(),
                Owner.Id.ToString(),
                Name,
                Location.Id.ToString(),
                Type.ToString(),
                MaxGuestNumber.ToString(),
                MinReservationDays.ToString(),
                CancellationThreshold.ToString(),
                ImageURLsCSV
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int i = 0;
            Id = int.Parse(values[i++]);
            Owner.Id = int.Parse(values[i++]);
            Name = values[i++];
            LocationController _locationController = new();
            Location = _locationController.GetByID(int.Parse(values[i++]));
            Type = values[i++];
            MaxGuestNumber = int.Parse(values[i++]);
            MinReservationDays = int.Parse(values[i++]);
            CancellationThreshold = int.Parse(values[i++]);
            ImageURLsCSV = values[i++];
            ImageURLsFromCSV(ImageURLsCSV);
        }
        public void ImageURLsToCSV()
        {
            if (ImageURLs.Count > 0)
            {
                ImageURLsCSV = string.Empty;
                foreach (var imageURL in ImageURLs) ImageURLsCSV += imageURL + ",";
                ImageURLsCSV = ImageURLsCSV.Remove(ImageURLsCSV.Length - 1);
            }
        }

        public void ImageURLsFromCSV(string value)
        {
            var imageURLs = value.Split(',');
            foreach (var imageURL in imageURLs) if (imageURL != string.Empty) ImageURLs.Add(imageURL);
        }

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && string.IsNullOrEmpty(Name)) return "Naziv je obavezan.";
                else if (columnName == "Type" && string.IsNullOrEmpty(Type)) return "Tip je obavezan.";
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Name", "Type" };
        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties) if (this[property] != null) return false;
                return true;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string? ToString()
        {
            return Name + " " + Location + " " + Type;
        }
    }
}
