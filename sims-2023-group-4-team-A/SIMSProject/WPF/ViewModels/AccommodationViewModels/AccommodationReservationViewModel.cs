using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationReservationViewModel : INotifyPropertyChanged
    {
        private readonly User _user;
        private readonly AccommodationReservationService _reservationService;
        public ObservableCollection<AccommodationReservation> Reservations = new();

        public AccommodationReservationViewModel(User user)
        {
            _user = user;
            _reservationService = Injector.GetService<AccommodationReservationService>();
        }

        public ObservableCollection<AccommodationReservation> LoadReservations()
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAll());
        }

        public ObservableCollection<AccommodationReservation> LoadReservationsByAccommodation(Accommodation accommodation)
        {
            return new ObservableCollection<AccommodationReservation>(_reservationService.GetAllByAccommodationId(accommodation.Id));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
