using SIMSProject.Application.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels
{
    public class TourManagerViewModel: ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly VoucherSevice _voucherService;
        private readonly TourGuestService _tourGuestService;

        public ObservableCollection<Tour> Tours { get; set; } = new();
        public ObservableCollection<TourAppointment> Appointments { get; set; } = new();

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
        public void GetTodaysTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetTodaysTours());
        }

        public void GetTours()
        {
            Tours.Clear();
            Tours = new(_tourService.GetTours());
        }

        public void GetAllAppointments()
        {
            Appointments.Clear();
            Appointments = new(_tourAppointmentService.GetAllByTourId(SelectedTour.Id));
        }

        public void CancelAppointment()
        {
            if (!_tourAppointmentService.CancelAppointment(SelectedAppointment))
            {
                MessageBox.Show("Greška! Možete otkazati termin najkasnije 48 sati pred početak!");
                return;
            }
            List<TourGuest> guests = _tourGuestService.GetGuests(SelectedAppointment);
            _voucherService.GiveVouchers(guests, ObtainingReason.APPOINTMENTCANCELED);

            MessageBox.Show("Uspešno ste otkazali termin.");
        }
        public TourManagerViewModel()
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _voucherService = Injector.GetService<VoucherSevice>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            _tourService = Injector.GetService<TourService>();
        }
    }
}
