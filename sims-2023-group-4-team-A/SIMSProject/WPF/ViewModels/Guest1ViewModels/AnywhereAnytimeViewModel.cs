using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMSProject.WPF.ViewModels.Guest1ViewModels
{
    public class AnywhereAnytimeViewModel: ViewModelBase
    {
        private User _user = new();
        private AccommodationService _accommodationService;
        private AccommodationReservation _accommodationReservation = new();
        public ObservableCollection<Accommodation> Accommodations { get; set; }
        public AccommodationReservation AccommodationReservation
        {
            get => _accommodationReservation;
            set
            {
                if (_accommodationReservation == value) return;
                _accommodationReservation = value;
                OnPropertyChanged();
            }
        }
        public int GuestsNumber
        {
            get => _accommodationReservation.GuestNumber;

            set
            {
                if (value != _accommodationReservation.GuestNumber && value >= 1)
                {
                    _accommodationReservation.GuestNumber = value;
                    OnPropertyChanged(nameof(GuestsNumber));
                }
            }
        }
        public int NumberOfDays
        {
            get => _accommodationReservation.NumberOfDays;

            set
            {
                if (value != _accommodationReservation.NumberOfDays && value >= 1)
                {
                    _accommodationReservation.NumberOfDays = value;
                    OnPropertyChanged(nameof(NumberOfDays));
                }
            }
        }
        public DateTime DateBegin
        {
            get => _accommodationReservation.StartDate;
            set
            {
                if (_accommodationReservation.StartDate == value) return;
                _accommodationReservation.StartDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateEnd
        {
            get => _accommodationReservation.EndDate;
            set
            {
                if (_accommodationReservation.EndDate == value) return;
                _accommodationReservation.EndDate = value;
                OnPropertyChanged();
            }
        }
        public AnywhereAnytimeViewModel(User user)
        {
            _user = user;
            _accommodationService = Injector.GetService<AccommodationService>();
            Accommodations = LoadAllAccommodations();
        }
        public ObservableCollection<Accommodation> LoadAllAccommodations()
        {
            return new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        }

        public void Search()
        {
            _accommodationService.SearchForFreeAccommodations(Accommodations, _user, DateBegin, DateEnd, NumberOfDays, GuestsNumber);
        }
    }
}
