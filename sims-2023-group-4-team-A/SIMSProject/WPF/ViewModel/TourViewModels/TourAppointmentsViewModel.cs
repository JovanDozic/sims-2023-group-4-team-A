using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Models.TourModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class TourAppointmentsViewModel
    {
        private readonly TourAppointmentService _tourAppointmentService = new();
        private readonly VoucherSevice _voucherService = new();
        private readonly TourGuestService _tourGuestService = new();

        public ObservableCollection<TourAppointment> Appointments { get; set; }
        public TourAppointment SelectedAppointment { get; set; } = new();

        public TourAppointmentsViewModel(Tour tour)
        {
            Appointments = new(_tourAppointmentService.GetAllByTourId(tour.Id));
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
    }
}
