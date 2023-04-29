using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels.BaseViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class TourAppointmentsViewModel : ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        public BaseTourViewModel Tour { get; set; }
        private ObservableCollection<TourAppointment> _appointments;
        public ObservableCollection<TourAppointment> Appointments
        {
            get => _appointments;
            set
            {
                if (_appointments == value) return;
                _appointments = value;
                OnPropertyChanged();
            }
        }

        private TourAppointment _selectedAppointment = new();
        public TourAppointment SelectedAppointment
        {
            get => _selectedAppointment;
            set
            {
                if (value != _selectedAppointment)
                {
                    _selectedAppointment = value;
                    OnPropertyChanged(nameof(SelectedAppointment));
                }
            }
        }

        public TourAppointmentsViewModel(Tour tour)
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            Appointments = new(_tourAppointmentService.GetAllByTourId(tour.Id));
            Tour = new(tour);
        }
        public ObservableCollection<TourAppointment> GetAllInactiveAppointments(Tour tour)
        {
            return Appointments = new(_tourAppointmentService.GetAllInactive(tour.Id));
        }

    }
}
