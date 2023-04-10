using SIMSProject.Application.Services.AccommodationServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SIMSProject.WPF.ViewModels.AccommodationViewModels
{
    public class AccommodationReservationViewModel : INotifyPropertyChanged
    {
        private Owner _owner;
        private readonly AccommodationReservationService _reservationService;
        public ObservableCollection<AccommodationReservation> Reservations = new();

        public AccommodationReservationViewModel(Owner owner)
        {
            _owner = owner;
            _reservationService = Injector.GetService<AccommodationReservationService>();
            LoadReservations();
        }

        public void LoadReservations()
        {
            Reservations = new ObservableCollection<AccommodationReservation>(_reservationService.GetAll());
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
