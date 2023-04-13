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

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class TourAppointmentsViewModel
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly VoucherSevice _voucherService;
        private readonly TourGuestService _tourGuestService;

        public BaseTourViewModel Tour { get; set; }
        public ObservableCollection<TourAppointment> Appointments { get; set; }
        public TourAppointment SelectedAppointment { get; set; } = new();

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
            TourAppointment? activeAppointment = Appointments.ToList().Find(x => x.TourStatus == Status.ACTIVE);
            if (activeAppointment == null)
            {
                SetStartPoint();
            }
            else if (activeAppointment.Id != SelectedAppointment.Id)
            {
                MessageBox.Show("Već postoji aktivna tura!");
            }
            ActivateAppointment();
        }
        private void ActivateAppointment()
        {
            _tourAppointmentService.ActivateAppointment(SelectedAppointment);
        }

        private void SetStartPoint()
        {
            SelectedAppointment.CurrentKeyPointId = Tour.KeyPoints[0].Id;
            SelectedAppointment.CurrentKeyPoint = Tour.KeyPoints.First();
        }
    }
}
