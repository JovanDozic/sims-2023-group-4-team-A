using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.WPF.ViewModels
{
    public class AccommodationViewModel : INotifyPropertyChanged
    {
        private Accommodation _model;
        public int Id
        {
            get => _model.Id;
            set
            {
                if (value == _model.Id) return;
                _model.Id = value;
                OnPropertyChanged();
            }
        }
        public Owner Owner
        {
            get => _model.Owner;
            set
            {
                if (value == _model.Owner) return;
                _model.Owner = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _model.Name;
            set
            {
                if (value == _model.Name) return;
                _model.Name = value;
                OnPropertyChanged();
            }
        }
        public Location Location
        {
            get => _model.Location;
            set
            {
                if (value == _model.Location) return;
                _model.Location = value;
                OnPropertyChanged();
            }
        }
        //public string Type
        //{
        //    get =>
        //        _model.Type switch
        //        {
        //            AccommodationType.Apartment => "Apartman",
        //            AccommodationType.House => "Kuća",
        //            _ => "Koliba"
        //        };
        //    set
        //    {
        //        _model.Type = value switch
        //        {
        //            "Apartman" => AccommodationType.Apartment,
        //            "Kuća" => AccommodationType.House,
        //            _ => AccommodationType.Hut
        //        };
        //        OnPropertyChanged();
        //    }
        //}
        public int MaxGuestNumber
        {
            get => _model.MaxGuestNumber;
            set
            {
                if (value == _model.MaxGuestNumber || value < 1) return;
                _model.MaxGuestNumber = value;
                OnPropertyChanged();
            }
        }
        public int MinReservationDays
        {
            get => _model.MinReservationDays;
            set
            {
                if (value == _model.MinReservationDays) return;
                _model.MinReservationDays = value;
                OnPropertyChanged();
            }
        }
        public int CancellationThreshold
        {
            get => _model.CancellationThreshold;
            set
            {
                if (value == _model.CancellationThreshold) return;
                _model.CancellationThreshold = value;
                OnPropertyChanged();
            }
        }
        public List<string> ImageURLs
        {
            get => _model.ImageURLs;
            set
            {
                if (value == _model.ImageURLs) return;
                _model.ImageURLs = value;
                OnPropertyChanged();
            }
        }
        public string ImageURLsCSV
        {
            get => _model.ImageURLsCSV;
            set
            {
                if (value == _model.ImageURLsCSV) return;
                _model.ImageURLsCSV = value;
                OnPropertyChanged();
            }
        }
        public List<AccommodationReservation> Reservations
        {
            get => _model.Reservations;
            set
            {
                if (value == _model.Reservations) return;
                _model.Reservations = value;
                OnPropertyChanged();
            }
        }

        public string[] ToCSV()
        {
            return _model.ToCSV();
        }

        public void FromCSV(string[] values)
        {
            _model.FromCSV(values);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

