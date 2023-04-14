using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class TourAppointmentsViewModel : ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly VoucherSevice _voucherService;
        private readonly TourGuestService _tourGuestService;

        public BaseTourViewModel Tour { get; set; }
        public ObservableCollection<TourAppointment> Appointments { get; set; }

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
            _voucherService = Injector.GetService<VoucherSevice>();
            _tourGuestService = Injector.GetService<TourGuestService>();

            Appointments = new(_tourAppointmentService.GetAllByTourId(tour.Id));
            Tour = new(tour);
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

        public void StartIfActivated()
        {
            TourAppointment? active = _tourAppointmentService.GetActiveByTour(Tour.Tour);
            if (active == null)
            {
                SelectedAppointment = _tourAppointmentService.Activate(SelectedAppointment, Tour.Tour);
            }
            else if (active.Id != SelectedAppointment.Id)
            {
                MessageBox.Show("Već postoji aktivna tura!");
                return;
            }
            else
            {
                SelectedAppointment = active;
            }
        }
    }
}
