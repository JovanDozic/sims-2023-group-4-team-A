using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationViewModel : ViewModelBase
    {
        private User _user;
        private Accommodation _accommodation = new();
        private AccommodationService _accommodationService;
        private LocationService _locationService;
        private string _selectedImageFile = string.Empty;
        private ObservableCollection<Accommodation> _accommodations = new();
        
        public ObservableCollection<string> AccommodationTypeSource { get; set; }
        public Accommodation Accommodation
        {
            get => _accommodation;
            set
            {
                if (_accommodation == value) return;
                _accommodation = value;
                OnPropertyChanged();
            }
        }
        public int Id
        {
            get => _accommodation.Id;
            set
            {
                if (_accommodation.Id == value) return;
                _accommodation.Id = value;
                OnPropertyChanged();
            }
        }
        public Owner Owner
        {
            get => _accommodation.Owner;
            set
            {
                if (_accommodation.Owner == value) return;
                _accommodation.Owner = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get => _accommodation.Name;
            set
            {
                if (_accommodation.Name == value) return;
                _accommodation.Name = value;
                OnPropertyChanged();
            }
        }
        public string LocationCity
        {
            get => _accommodation.Location.City;
            set
            {
                if (_accommodation.Location.City == value) return;
                _accommodation.Location.City = value;
                OnPropertyChanged();
            }
        }
        public string LocationCountry
        {
            get => _accommodation.Location.Country;
            set
            {
                if (_accommodation.Location.Country == value) return;
                _accommodation.Location.Country = value;
                OnPropertyChanged();
            }
        }
        public AccommodationType Type
        {
            get => _accommodation.Type;
            set
            {
                if (_accommodation.Type == value) return;
                _accommodation.Type = value;
                OnPropertyChanged();
            }
        }
        public int MaxGuestNumber
        {
            get => _accommodation.MaxGuestNumber;
            set
            {
                if (_accommodation.MaxGuestNumber == value || value < 1) return;
                _accommodation.MaxGuestNumber = value;
                OnPropertyChanged();
            }
        }
        public int MinReservationDays
        {
            get => Accommodation.MinReservationDays;
            set
            {
                if (Accommodation.MinReservationDays == value || value < 1) return;
                Accommodation.MinReservationDays = value;
                OnPropertyChanged();
            }
        }
        public int CancellationThreshold
        {
            get => Accommodation.CancellationThreshold;
            set
            {
                if (Accommodation.CancellationThreshold == value || value < 1) return;
                Accommodation.CancellationThreshold = value;
                OnPropertyChanged();
            }
        }
        public List<string> ImageURLs
        {
            get => Accommodation.ImageURLs;
            set
            {
                if (Accommodation.ImageURLs == value) return;
                Accommodation.ImageURLs = value;
                OnPropertyChanged();
            }
        }
        public string ImageURLsCSV
        {
            get => Accommodation.ImageURLsCSV;
            set
            {
                if (Accommodation.ImageURLsCSV == value) return;
                Accommodation.ImageURLsCSV = value;
                OnPropertyChanged();
            }
        }
        public string FeaturedImage
        {
            get => Accommodation.FeaturedImage;
            set
            {
                if (Accommodation.FeaturedImage == value) return;
                Accommodation.FeaturedImage = value;
                OnPropertyChanged();
            }
        }
        public string SelectedImageFile
        {
            get => _selectedImageFile;
            set
            {
                if (_selectedImageFile == value) return;
                _selectedImageFile = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Accommodation> Accommodations
        {
            get => _accommodations;
            set
            {
                if (value == _accommodations) return;
                _accommodations = value;
                OnPropertyChanged();
            }
        }

        public AccommodationViewModel(User user)
        {
            _user = user;
            _accommodationService = Injector.GetService<AccommodationService>();
            _locationService = Injector.GetService<LocationService>();

            AccommodationTypeSource = new ObservableCollection<string>
            {
                Accommodation.GetType(AccommodationType.Apartment),
                Accommodation.GetType(AccommodationType.House),
                Accommodation.GetType(AccommodationType.Hut)
            };
        }

        public ObservableCollection<Accommodation> LoadAccommodationsByOwner()
        {
            _accommodationService.ReloadAccommodations();
            return Accommodations = new(_accommodationService.GetAllByOwnerId(_user.Id));
        }

        public void RegisterAccommodation()
        {
            _accommodation.Location = _locationService.GetLocation(LocationCity, LocationCountry);
            _accommodation.Owner = _user as Owner ?? throw new Exception("Greška prilikom registrovanja: Vlasnik nije inicijalizovan.");
            _accommodationService.RegisterAccommodation(_accommodation);
        }

        public void UploadImageToAccommodation(string imageUrl)
        {
            ImageURLs.Add(imageUrl);
        }
    }
}

