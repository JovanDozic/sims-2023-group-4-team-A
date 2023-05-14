using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Application.Services;
using SIMSProject.Domain.Models;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class ToursViewModel: ViewModelBase
    {
        private Guest _user;
        private readonly TourService _tourService;
        private readonly TourGuestService _tourGuestService;
        public ObservableCollection<Tour> Tours { get; set; }

        private Tour _tour = new();
        public Tour SelectedTour
        {   
            get => _tour;
            set
            {
                if(value != _tour)
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
        public string TourLanguage
        {
            get
            {
                return _tour.TourLanguage switch
                {
                    Language.ENGLISH => "Engleski",
                    Language.SERBIAN => "Srpski",
                    Language.SPANISH => "Španski",
                    _ => "Francuski"

                };
            }
            set
            {
                _tour.TourLanguage = value switch
                {
                    "Engleski" => Language.ENGLISH,
                    "Srpski" => Language.SERBIAN,
                    "Španski" => Language.SPANISH,
                    _ => Language.FRENCH
                };
                OnPropertyChanged();
            }
        }
        public void Search(string locationAndLanguage, string language)
        {
           _tourService.SearchTours(locationAndLanguage, DurationSearch, GuestSearch, language, Tours);
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
        public ToursViewModel(Guest user)
        {
            _user = user;
            _tourService = Injector.GetService<TourService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            Tours = new(_tourService.GetTours());
        }
    }
}
