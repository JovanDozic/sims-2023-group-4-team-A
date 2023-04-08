using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SIMSProject.Model;
using SIMSProject.Observer;

namespace SIMSProject.Model
{
    public class AccommodationAndOwnerRating : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        private int _cleanlinessRating = 1;
        public int CleanlinessRating
        {
            get => _cleanlinessRating;
            set
            {
                if (_cleanlinessRating == value || value is < 1 or > 5) return;
                _cleanlinessRating = value;
                OnPropertyChanged();
            }
        }

        private int _ownerCorrectness = 1;
        public int OwnerCorrectness
        {
            get => _ownerCorrectness;
            set
            {
                if (_ownerCorrectness == value || value is < 1 or > 5) return;
                _ownerCorrectness = value;
                OnPropertyChanged();
            }
        }
        private int _kindness = 1;
        public int Kindness
        {
            get => _kindness;
            set
            {
                if (value == _kindness || value is < 1 or > 5) return;
                _kindness = value;
                OnPropertyChanged();
            }
        }


        private string _comment = string.Empty;
        public string Comment
        {
            get => _comment;
            set
            {
                if (_comment == value) return;

                _comment = value;
                OnPropertyChanged();
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
                    OnPropertyChanged();
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
                    OnPropertyChanged();
                }
            }
        }

        public AccommodationAndOwnerRating()
        {
        }

        public AccommodationAndOwnerRating(int id, AccommodationReservation accommodationReservation, int cleanlinessRating, int ownerCorrectness, string comment, List<string> imageURLs, string imageURLsCSV)
        {
            Id = id;
            AccommodationReservation = accommodationReservation;
            CleanlinessRating = cleanlinessRating;
            OwnerCorrectness = ownerCorrectness;
            Comment = comment;
            ImageURLs = imageURLs;
            ImageURLsCSV = imageURLsCSV;
            ImageURLsFromCSV(imageURLsCSV);
            
        }

        public void ImageURLsFromCSV(string value)
        {
            var imageURLs = value.Split(',');
            foreach (var imageURL in imageURLs)
            {
                if (imageURL != string.Empty)
                {
                    ImageURLs.Add(imageURL);
                }
            }
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                CleanlinessRating.ToString(),
                OwnerCorrectness.ToString(),
                Comment
                

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationReservation.Id = int.Parse(values[1]);
            CleanlinessRating = int.Parse(values[2]);
            OwnerCorrectness = int.Parse(values[3]);
            Comment = values[4];
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
