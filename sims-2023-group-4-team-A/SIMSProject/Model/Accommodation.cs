using SIMSProject.Controller;
using SIMSProject.Serializer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace SIMSProject.Model
{
    public enum ACCOMMODATION_TYPE { APARTMENT = 0, HOUSE, HUT };
    public class Accommodation : ISerializable, INotifyPropertyChanged, IDataErrorInfo
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
        public List<string> ImageURLs;
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

        public Accommodation()
        {
            Location = new();
            ImageURLs = new();
        }

        public Accommodation(string name, Address location, string type, int maxGuestNumber, int minReservationDays, int cancellationThreshold, string imageURLsCSV)
        {
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
        
    

        // [SERIALIZATION HANDLING]

        public string[] ToCSV()
        {
            ImageURLsToCSV();
            string[] csvValues = {
                Id.ToString(),
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
            Id = int.Parse(values[0]);
            Name = values[1];
            AddressController addressController = new();
            Location = addressController.GetById(int.Parse(values[2]));
            Type = values[3];
            MaxGuestNumber = int.Parse(values[4]);
            MinReservationDays = int.Parse(values[5]);
            CancellationThreshold = int.Parse(values[6]);
            ImageURLsCSV = values[7];
            ImageURLsFromCSV(ImageURLsCSV);

            Trace.Write("\nAccommodation [" + Id + "] has " + ImageURLs.Count + " images: ");
            foreach (var imageURL in ImageURLs) Trace.Write(imageURL + " --- ");
        }
        private void ImageURLsToCSV()
        {
            if (ImageURLs.Count > 0)
            {
                ImageURLsCSV = string.Empty;
                foreach (var imageURL in ImageURLs) ImageURLsCSV += imageURL + ",";
                ImageURLsCSV = ImageURLsCSV.Remove(ImageURLsCSV.Length - 1);
            }
        }

        private void ImageURLsFromCSV(string value)
        {
            var imageURLs = value.Split(',');
            foreach (var imageURL in imageURLs) if (imageURL != string.Empty) ImageURLs.Add(imageURL);
        }


        // [VALIDATION HANDLING]

        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name" && string.IsNullOrEmpty(Name)) return "Naziv je obavezan.";
                else if (columnName == "Type" && string.IsNullOrEmpty(Type)) return "Tip je obavezan.";
                else if (columnName == "ImageURLsCSV" && string.IsNullOrEmpty(ImageURLsCSV)) return "Bar jedna slika je obavezna.";
                return null;
            }
        }
        private readonly string[] _validatedProperties = { "Name", "Type", "ImageURLsCSV" };
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

        public override string? ToString()
        {
            return Name + " " + Location + " " + Type;
        }
    }

}
