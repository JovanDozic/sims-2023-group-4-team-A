using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.ObjectModel;

namespace SIMSProject.WPF.ViewModels.Guest2ViewModels
{
    public class TourReservationsViewModel : ViewModelBase
    {
        private readonly User _user;
        private readonly TourReservationService _reservationService;
        private readonly TourGuestService _tourGuestService;
        public ObservableCollection<TourReservation> _tourReservations = new();
        public ObservableCollection<TourReservation> TourReservations
        {
            get => _tourReservations;
            set
            {
                if (value == _tourReservations) return;
                _tourReservations = value;
                OnPropertyChanged();
            }
        }
        private TourReservation _selectedTourReservation = new();
        public TourReservation SelectedTourReservation
        {
            get => _selectedTourReservation;
            set
            {
                if (value == _selectedTourReservation) return;
                _selectedTourReservation = value;
                OnPropertyChanged(nameof(SelectedTourReservation));
            }
        }
        private TourReservation _tourReservation = new();
        public TourReservation TourReservation
        {
            get => _tourReservation;
            set
            {
                if (value == _tourReservation) return;
                _tourReservation = value;
                OnPropertyChanged(nameof(TourReservation));
            }
        }
        public DateTime Date
        {
            get => _selectedTourReservation.TourAppointment.Date;
            set
            {
                if (value != _selectedTourReservation.TourAppointment.Date)
                {
                    _selectedTourReservation.TourAppointment.Date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        //public string KeyPoints { get => _selectedTourReservation.TourAppointment.Tour.KeyPointsToString(); }
        public TourReservationsViewModel(User user)
        {
            _user = user;
            //TourReservation = tourReservation;
            _reservationService = Injector.GetService<TourReservationService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
        }

        public void LoadReservationsByGuestId(int GuestId)
        {
            TourReservations = new(_reservationService.GetAllByGuestId(GuestId));
        }

        public int GetGuideId()
        {
            return SelectedTourReservation.TourAppointment.Tour.Guide.Id;
        }

        public bool IsRatingEnabled()
        {
            if (SelectedTourReservation == null) return false;
            var tourGuest = _tourGuestService.GetGuest(SelectedTourReservation.TourAppointment, _user.Id);
            if (SelectedTourReservation.TourAppointment.TourStatus == Status.COMPLETED && SelectedTourReservation.GuideRated == false && tourGuest.GuestStatus == GuestAttendance.PRESENT)
            {
                return true;
            }
            return false;
        }
        
        public bool IsTourActive()
        {
            if (SelectedTourReservation == null) return false;
            //if (SelectedTourReservation.TourAppointment.TourStatus == Status.ACTIVE) return true;
            return true;
        }
        public void GetDetails(TourReservation tourReservation)
        {
            TourReservation = tourReservation;
        }
    }
}
