using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using SIMSProject.Domain.Models.UserModels;

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
        public void Search(string locationAndLanguage, int searchDuration, int searchMaxGuests)
        {
           _tourService.SearchTours(locationAndLanguage, searchDuration, searchMaxGuests, Tours);
        }

        public bool IsSelected()
        {
            return SelectedTour != null;
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
