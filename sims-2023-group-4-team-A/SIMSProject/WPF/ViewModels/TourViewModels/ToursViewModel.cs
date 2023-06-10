using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMSProject.Domain.Models.UserModels;
using System.Windows.Input;
using System.Windows;
using SIMSProject.View.Guest2;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class ToursViewModel : ViewModelBase
    {
        private Guest2 _user;
        private readonly TourService _tourService;
        private readonly TourGuestService _tourGuestService;
        public ObservableCollection<Tour> Tours { get; set; }

        private Tour _tour = new();
        public Tour SelectedTour
        {
            get => _tour;
            set
            {
                if (value != _tour)
                {
                    _tour = value;
                    OnPropertyChanged(nameof(SelectedTour));
                }
            }
        }
        private int _guestSearch;
        public int GuestSearch
        {
            get => _guestSearch;
            set
            {
                if (value == _guestSearch || value is < 0) return;
                _guestSearch = value;
                OnPropertyChanged(nameof(GuestSearch));
            }
        }
        private int _durationSearch;
        public int DurationSearch
        {
            get => _durationSearch;
            set
            {
                if (value == _durationSearch || value is < 0) return;
                _durationSearch = value;
                OnPropertyChanged(nameof(DurationSearch));
            }
        }
        private string _locationSearch = "Gde putujete?";
        public string LocationSearch
        {
            get => _locationSearch;
            set
            {
                if (value == _locationSearch) return;
                _locationSearch = value;
                OnPropertyChanged(nameof(LocationSearch));
            }
        }
        
        private string _tourLanguage;
        public string TourLanguage
        {
            get => _tourLanguage;
            set
            {
                if (value == _tourLanguage) return;
                _tourLanguage = value;
                OnPropertyChanged(nameof(TourLanguage));
            }
        }

        private Visibility _labelVisibility;
        public Visibility LabelVisibility
        {
            get => _labelVisibility;
            set
            {
                if(value == _labelVisibility) return;
                _labelVisibility = value;
                OnPropertyChanged(nameof(LabelVisibility));
            }
        }
        public ObservableCollection<string> TourLanguages { get; set; }
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand ReserveCommand { get; set; }
        public RelayCommand TextSearch_GotFocusCommand { get; set; }
        public RelayCommand TextSearch_LostFocusCommand { get; set; }

        public ToursViewModel(Guest2 user)
        {
            _user = user;
            _tourService = Injector.GetService<TourService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            Tours = new(_tourService.GetTours());
            TourLanguages = new(Tour.GetLanguages());
            LabelVisibility = Visibility.Hidden;
            SearchCommand = new RelayCommand(SearchExecute);
            //ReserveCommand = new RelayCommand(ReserveExecute);

            //TextSearch_GotFocusCommand = new RelayCommand(TextSearch_GotFocus);
            //TextSearch_LostFocusCommand = new RelayCommand(TextSearch_LostFocus);
        }
        public void SearchExecute()
        {
            TourLanguage = ConvertLanguage(TourLanguage);
            LabelVisibility = Visibility.Hidden;
            _tourService.SearchTours(LocationSearch, DurationSearch, GuestSearch, TourLanguage, Tours);
        }

        public void ReserveExecute()
        {
            if (IsSelected())
            {
                new TourReservationCreation(_user, SelectedTour).Show();
                //new ReservationCreation(_user, SelectedTour);
                LabelVisibility = Visibility.Hidden;
                return;
            }
            LabelVisibility = Visibility.Visible;
        }


        private string ConvertLanguage(string selectedLanguage)
        {
            switch (selectedLanguage)
            {
                case "Srpski":
                    return "SERBIAN";
                case "Engleski":
                    return "ENGLISH";
                case "Španski":
                    return "SPANISH";
                case "Francuski":
                    return "FRENCH";
                default:
                    return "";
            }
        }
        public bool IsSelected()
        {
            if(SelectedTour.Id == 0) return false;
            return true;
        }
        public List<TourGuest> GetPendingTourGuests(User user)
        {
            return _tourGuestService.GetAllPendingByUser(user);
        }
        public void ConfirmTourGuestAttendance(TourGuest tourGuest)
        {
            _tourGuestService.MakeGuestPresent(tourGuest);

        }
        
    }
}
