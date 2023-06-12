using Microsoft.Win32;
using SIMSProject.Application.Services;
using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationViewModel : ViewModelBase, IDataErrorInfo
    {
        private User _user;
        private App _app = (App)System.Windows.Application.Current;
        private OwnerRegisterAccommodationView? _registerView;

        private Accommodation _accommodation = new();
        private AccommodationService _accommodationService;
        private LocationService _locationService;
        private AccommodationReservationService _accommodationReservationService;
        private AccommodationReservationViewModel _accommodationReservationViewModel;
        private AccommodationRenovationService _renovationService;
        private string _fullLocation = string.Empty;
        private string _selectedImageFile = string.Empty;
        private Accommodation _selectedAccommodation = new();
        private string _maxGuestNumberString = string.Empty;
        private string _minReservationDaysString = string.Empty;
        private string _cancellationThresholdString = string.Empty;
        private ObservableCollection<Accommodation> _accommodations = new();

        public ObservableCollection<AccommodationType> AccommodationTypeSource { get; set; }
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value == _selectedAccommodation) return;
                _selectedAccommodation = value;
                OnPropertyChanged();
            }
        }
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
        public string FullLocation
        {
            get => _fullLocation;
            set
            {
                if (value == _fullLocation) return;
                _fullLocation = value;
                PrepareLocation();
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
        public string MaxGuestNumberString
        {
            get => _maxGuestNumberString;
            set
            {
                if (value == _maxGuestNumberString) return;
                if (value == string.Empty) _maxGuestNumberString = value;
                if (!int.TryParse(value, out int result)) return;
                _maxGuestNumberString = value;
                _accommodation.MaxGuestNumber = result;
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
        public string MinReservationDaysString
        {
            get => _minReservationDaysString;
            set
            {
                if (value == _minReservationDaysString) return;
                if (value == string.Empty) _minReservationDaysString = value;
                if (!int.TryParse(value, out int result)) return;
                _minReservationDaysString = value;
                _accommodation.MinReservationDays = result;
                OnPropertyChanged();
            }
        }
        public int MinReservationDays
        {
            get => _accommodation.MinReservationDays;
            set
            {
                if (_accommodation.MinReservationDays == value || value < 1) return;
                Accommodation.MinReservationDays = value;
                OnPropertyChanged();
            }
        }
        public string CancellationThresholdString
        {
            get => _cancellationThresholdString;
            set
            {
                if (value == _cancellationThresholdString) return;
                if (value == string.Empty) _cancellationThresholdString = value;
                if (!int.TryParse(value, out int result)) return;
                _cancellationThresholdString = value;
                _accommodation.CancellationThreshold = result;
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
        public string Description
        {
            get => Accommodation.Description;
            set
            {
                if (Accommodation.Description == value) return;
                Accommodation.Description = value;
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
        public double Rating
        {
            get => Accommodation.Rating.Overall;
            set
            {
                OnPropertyChanged();
            }
        }
        public bool IsRecentlyRenovated
        {
            get => Accommodation.IsRecentlyRenovated;
            set
            {
                if (value == Accommodation.IsRecentlyRenovated) return;
                Accommodation.IsRecentlyRenovated = value;
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

        public RelayCommand RegisterAccommodationCommand { get; }
        public RelayCommand PrepareLocationCommand { get; }
        public RelayCommand UploadImageToAccommodationCommand { get; }

        public AccommodationViewModel(User user, OwnerRegisterAccommodationView? ownerRegisterAccommodationView = null)
        {
            _user = user;
            _accommodationService = Injector.GetService<AccommodationService>();
            _locationService = Injector.GetService<LocationService>();
            _accommodationReservationService = Injector.GetService<AccommodationReservationService>();
            _renovationService = Injector.GetService<AccommodationRenovationService>();
            _accommodationReservationViewModel = new(_user);
            Accommodations = LoadAllAccommodations();
            _registerView = ownerRegisterAccommodationView;

            AccommodationTypeSource = new ObservableCollection<AccommodationType>
            {
                AccommodationType.Apartment,
                AccommodationType.House,
                AccommodationType.Hut
            };
            Type = _accommodationService.GetMostUsedTypeBuUser(_user);

            RegisterAccommodationCommand = new(RegisterAccommodation, CanRegisterAccommodation);
            PrepareLocationCommand = new(PrepareLocation, CanPrepareLocation);
            UploadImageToAccommodationCommand = new(UploadImageToAccommodation, CanUploadIMageToAccommodation);
        }

        public bool IsNotSelected()
        {
            return SelectedAccommodation == null;
        }

        public ObservableCollection<Accommodation> LoadAllAccommodations()
        {
            return new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        }

        public ObservableCollection<Accommodation> LoadAccommodationsByOwner()
        {
            _accommodationService.ReloadAccommodations();
            return Accommodations = new(_accommodationService.GetAllByOwnerId(_user.Id));
        }

        public void Search(string nameTypeLocation,int duration, int maxGuests)
        {
            _accommodationService.Search(Accommodations,nameTypeLocation, duration, maxGuests);
        }

        public void RegisterAccommodation()
        {
            MessageBoxResult result;
            if (_app.CurrentLanguage == "en-US") 
                result = MessageBox.Show("Are you sure you want to register accommodation?", "Confirm registration", MessageBoxButton.YesNo, MessageBoxImage.Question);
            else result = MessageBox.Show("Da li ste sigurni da želite da registrujete smeštaj?", "Potvrdite registraciju", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            _accommodation.Location = _locationService.GetLocation(_accommodation.Location);
            if (_accommodation.Location == null) return;
            _accommodation.Owner = _user as Owner ?? throw new Exception("Error! Owner is not initialized.");
            _accommodationService.RegisterAccommodation(_accommodation);

            _registerView?.GoBackAndReload();
        }
        public string GetDateMessage()
        {
            return "Krajnji datum mora biti veći od početnog datuma";
        }

        public string GetDaysMessage()
        {
            return $"Minimalan broj dana je {SelectedAccommodation.MinReservationDays}";
        }
        public string GetDaysDurationMessage()
        {
            return "Broj dana se ne poklapa sa opsegom datuma";
        }
        public string GetGuestsMessage()
        {
            return $"Maksimalan broj gostiju je {SelectedAccommodation.MaxGuestNumber}";
        }
        public bool IsDateInPast(DateTime dateBegin, DateTime dateEnd)
        {
            return dateBegin >= dateEnd;
        }

        public bool IsNumberOfDaysValid(int numberOfDays)
        {
            return numberOfDays >= SelectedAccommodation.MinReservationDays;
        }

        public bool IsNumberOfDaysGreaterThanDuration(int numberOfDays, TimeSpan duration)
        {
            return numberOfDays <= duration.Days;
        }

        public bool IsAccommodationOccupied(DateTime dateBegin, DateTime dateEnd)
        {
            return IsReserved(dateBegin, dateEnd) || IsRenovating(dateBegin, dateEnd);
        }

        public bool IsReserved(DateTime dateBegin, DateTime dateEnd)
        {
            return _accommodationService.CheckReservations(_accommodationReservationService.GetAllUncanceledByAccommodationId(SelectedAccommodation.Id), dateBegin, dateEnd).Count != 0;
        }

        public bool IsRenovating(DateTime dateBegin, DateTime dateEnd)
        {
            return _accommodationService.CheckRenovations(_renovationService.GetAllUncanceledByAccommodationId(SelectedAccommodation.Id), dateBegin, dateEnd).Count != 0;
        }

        public bool IsGuestsNumberValid(int guestsNumber)
        {
            return guestsNumber <= SelectedAccommodation.MaxGuestNumber;
        }

        public List<DateRange> GetReservedDates()
        {
            List<DateRange> dateRanges = new List<DateRange>();
            foreach (var reservation in RemoveCancelled(_accommodationReservationService.GetAllByAccommodationId(SelectedAccommodation.Id)))
            {
                dateRanges.Add(new DateRange(reservation.StartDate, reservation.EndDate));
            }
            foreach(var renovation in _renovationService.GetAllUncanceledByAccommodationId(SelectedAccommodation.Id))
            {
                dateRanges.Add(new DateRange(renovation.StartDate, renovation.EndDate));
            }
            return dateRanges;
        }

        public List<AccommodationReservation> RemoveCancelled(List<AccommodationReservation> reservedAccommodations)
        {
            reservedAccommodations.RemoveAll(reserved => reserved.Canceled);
            return reservedAccommodations;
        }
        public bool CanRegisterAccommodation()
        {
            return IsAccommodationValid;
        }

        public void UploadImageURLToAccommodation(string imageUrl)
        {
            ImageURLs.Add(imageUrl);
        }

        public void PrepareLocation()
        {
            if (!CanPrepareLocation()) return;
            string[] parts = FullLocation.Split(',');
            _accommodation.Location.City = parts[0].Trim();
            _accommodation.Location.Country = parts[1].Trim();
        }

        public bool CanPrepareLocation()
        {
            return FullLocation.Split(',').Length == 2;
        }

        public void UploadImageToAccommodation()
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.DefaultExt = ".png";
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

            bool? result = openFileDialog.ShowDialog();
            if (result is not true || result is null) return;
            if (ImageURLs.Find(x => x.Equals(openFileDialog.FileName)) != null) return;
            ImageURLs.Add(openFileDialog.FileName);
        }

        private bool CanUploadIMageToAccommodation()
        {
            return true;
        }

        internal void SearchAccommodations(string text)
        {
            LoadAccommodationsByOwner();
            var SearchResults = Accommodations.ToList();
            SearchResults.RemoveAll(x => !x.ToStringSearchable.ToLower().Contains(text.ToLower()));
            Accommodations = new(SearchResults);
        }

        internal void LoadFirstTwoAccommodationsByOwner()
        {
            Accommodations = new(_accommodationService.GetAllByOwnerId(_user.Id).Take(2));
        }

        public string this[string columnName]
        {
            get
            {
                string? error = null;
                string requiredMessage = "Obavezno polje";
                switch (columnName)
                {
                    case nameof(Name):
                        if (string.IsNullOrEmpty(Name)) error = requiredMessage;
                        else if (Name.Length < 3) error = "Ime mora biti duže od 3 karaktera";
                        else if (Name.Contains("|")) error = "Unos ne sme da sadrzi '|'";
                        break;
                    case nameof(FullLocation):
                        if (string.IsNullOrEmpty(FullLocation)) error = requiredMessage;
                        if (!FullLocation.Contains(',')) error = requiredMessage;
                        var parts = FullLocation.Split(',');
                        if (FullLocation.Length < 3 || parts.Length != 2) error = "Lokacija mora biti u formatu 'Grad, Država'";
                        else if (parts[0].Length < 3 || parts[1].Length < 3) error = "Imena grada i države moraju biti duže od 3 karaktera";
                        else if (!_locationService.Exists(parts[0], parts[1])) error = "Lokacija ne postoji u nasem sistemu";
                        break;
                    case nameof(Type):
                        if (string.IsNullOrEmpty(Accommodation.GetType(Type)) || Type == AccommodationType.None) error = requiredMessage;
                        break;
                    case nameof(MaxGuestNumberString):
                        if (string.IsNullOrEmpty(MaxGuestNumberString)) error = requiredMessage;
                        if (MaxGuestNumber <= 0) error = "Maksimalan broj gostiju mora biti veći od 0";
                        break;
                    case nameof(MinReservationDaysString):
                        if (string.IsNullOrEmpty(MinReservationDaysString)) error = requiredMessage;
                        if (MinReservationDays <= 0) error = "Minimalan broj dana za rezervaciju mora biti veći od 0";
                        break;
                    case nameof(CancellationThresholdString):
                        if (string.IsNullOrEmpty(CancellationThresholdString)) error = requiredMessage;
                        if (CancellationThreshold <= 0) error = "Otkazni rok mora biti veći od 0";
                        break;
                    case nameof(Description):
                        if (string.IsNullOrEmpty(Description)) error = requiredMessage;
                        else if (Description.Length < 20) error = "Opis mora biti duži od 20 karaktera";
                        else if (Description.Contains("|")) error = "Unos ne sme da sadrzi '|'";
                        break;
                    case nameof(ImageURLs):
                        if (ImageURLs.Count <= 0) error = requiredMessage;
                        break;
                    default:
                        break;
                }
                return error;
            }
        }
        public string Error => null;
        public bool IsAccommodationValid
        {
            get
            {
                foreach (var property in new string[] {
                    nameof(Name),
                    nameof(FullLocation),
                    nameof(Type),
                    nameof(MaxGuestNumberString),
                    nameof(MinReservationDaysString),
                    nameof(CancellationThresholdString),
                    nameof(Description),
                    nameof(ImageURLs)})
                {
                    if (this[property] != null) return false;
                }
                return true;
            }
        }
    }
}

