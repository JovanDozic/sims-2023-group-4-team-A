using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationDetailsViewModel : ViewModelBase
    {
        private readonly User _user;
        private Accommodation _accommodation = new();
        private ObservableCollection<AccommodationReservation> _reservations = new();
        private readonly AccommodationReservationService _reservationService;
        
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

        public ObservableCollection<AccommodationReservation> Reservations
        {
            get => _reservations;
            set
            {
                if (_reservations == value) return;
                _reservations = value;
                OnPropertyChanged();
            }
        } 

        public AccommodationDetailsViewModel(User user, Accommodation accommodation)
        {
            _user = user;
            Accommodation = accommodation;

            _reservationService = Injector.GetService<AccommodationReservationService>();

            Reservations = new(_reservationService.GetAllByAccommodationId(Accommodation.Id));
        }




    }
}
