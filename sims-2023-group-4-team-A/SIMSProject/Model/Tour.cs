using SIMSProject.Serializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using SIMSProject.Controller;
using SIMSProject.Repository;

public enum Language { ENGLISH = 0, SERBIAN, SPANISH, FRENCH };

namespace SIMSProject.Model
{
    public class Tour : ISerializable, IDataErrorInfo, INotifyPropertyChanged
    {
        public int Id { get; set; }
        private string? _name;
        public string Name 
        { 
            get => _name; 
            set 
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            } 
        }
        public User Guide { get; set; }
        public Location Location { get; set; } = new Location();
        private string? _description;
        public string Description
        {
            get => _description;
            set
            {
                if(value != _description)
                {
                    _description = value; 
                    OnPropertyChanged(nameof(Description));
                }
            }
        }
        private Language _language;
        public string TourLanguage
        {
            get 
            {
                return _language switch
                {
                    Language.ENGLISH => "Engleski",
                    Language.SERBIAN => "Srpski",
                    Language.SPANISH => "Španski",
                    _ => "Francuski"
                };
            }
            set
            {
                _language = value switch
                {
                    "Engleski" => Language.ENGLISH,
                    "Srpski" => Language.SERBIAN,
                    "Španski" => Language.SPANISH,
                    _ => Language.FRENCH
                };
            }
        }

        private int _maxGuestNumber;
        public int MaxGuestNumber 
        { 
            get => _maxGuestNumber;
            set
            {
                if(_maxGuestNumber != value && value >= 1)
                {
                    _maxGuestNumber = value;
                    OnPropertyChanged(nameof(MaxGuestNumber));
                }
            }
        }

        private int _availableSpots;
        public int AvailableSpots
        {
            get => _availableSpots;
            set
                {
                if(_availableSpots != value)
                {
                    _availableSpots = value;
                    OnPropertyChanged(nameof(AvailableSpots));
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if(_duration != value && value >= 1)
                {
                    _duration = value;
                    OnPropertyChanged(nameof(Duration));
                }
            }
        }

        private int _locationId;
        public int LocationId
        {
            get => _locationId;
            set
            {
                if (value != _locationId)
                {
                    _locationId = value;
                    OnPropertyChanged(nameof(LocationId));
                }
            }
        }

        private int _guideId;
        public int GuideId
        {
            get => _guideId;
            set
            {
                if (_guideId != value)
                {
                    _guideId = value;
                    OnPropertyChanged(nameof(GuideId));
                }
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public List<KeyPoint> KeyPoints { get; set; } = new List<KeyPoint>();

        public List<TourDate> Dates { get; set; } = new List<TourDate>();

        public List<String> Images { get; set; } = new List<String>();

        public List<User> Sightseers { get; set; } = new List<User>();

        
        public Tour() { }

        public Tour(int id, string name, User guide, Location location, string description, string tourLanguage, int maxGuestNumber, int availableSpots,int duration, int locationId, int guideId)
        {
            Id = id;
            Name = name;
            Guide = guide;
            Location = location;
            Description = description;
            TourLanguage = tourLanguage;
            MaxGuestNumber = maxGuestNumber;
            AvailableSpots = availableSpots;
            Duration = duration;
            LocationId = locationId;
            GuideId = guideId;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Description = values[2];
            TourLanguage = values[3];
            MaxGuestNumber = Convert.ToInt32(values[4]);
            AvailableSpots = Convert.ToInt32(Convert.ToInt32(values[5]));
            Duration = Convert.ToInt32(values[6]);
            LocationId = Convert.ToInt32(values[7]);
            GuideId = Convert.ToInt32(values[8]);
            string[] ImageURLs = values[9].Split(',');
            Images.AddRange(ImageURLs);

        }



        private StringBuilder CreateImageURLs()
        {
            StringBuilder imageURLs = new StringBuilder();
            foreach (string imageURL in Images)
            {
                imageURLs.Append(imageURL + ",");
            }
            imageURLs.Remove(imageURLs.Length - 1, 1);
            return imageURLs;
        }
        public string[] ToCSV()
        {
            
            StringBuilder imageURLs = CreateImageURLs();

            string[] csvValues = { Id.ToString(), Name, Description, TourLanguage, MaxGuestNumber.ToString(), AvailableSpots.ToString(),Duration.ToString(), LocationId.ToString(), GuideId.ToString(), imageURLs.ToString() };
            return csvValues;
        }

        /*Validation*/
        Regex NameReg = new Regex("^[a-zA-Z]+(([',. -][a-zA-Z ])?[a-zA-Z]*)*$");
        Regex MaxGuestsReg = new Regex("^[0-9]+$");
        Regex DurationReg = new Regex("^[0-9]{1}");
        Regex DescriptionReg = new Regex("^\\w+(\\s+\\w+){2,}$");

        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        return "Naziv";

                    if (!NameReg.IsMatch(Name))
                        return "Ne moze biti naziv ture";
                }
                else if (columnName == "MaxGuestNumber")
                {
                    if (MaxGuestNumber <= 0)
                        return "Broj";
                }
                else if (columnName == "Duration")
                {
                    if (Duration <= 0)
                        return "Trajanje";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Obavezno uneti opis ture";
                    if (!DescriptionReg.IsMatch(Description))
                        return "Opis mora sadržati bar 3 reči";
                }
                    


                return null;
            }
        }

        private readonly string[] _validatesProperties = { "Name", "Duration", "MaxGuestNumber", "Description"};

        

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

        public override string? ToString()
        {
            return Location + " " + TourLanguage.ToString();
        }
    }
}
