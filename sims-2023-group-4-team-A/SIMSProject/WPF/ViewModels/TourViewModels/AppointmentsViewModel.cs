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
    public class AppointmentsViewModel: ViewModelBase
    {
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly VoucherSevice _voucherService;
        private readonly TourGuestService _tourGuestService;
        private readonly TourService _tourService;

        public BaseTourViewModel Tour { get; set; }
        public BaseAppointmentViewModel Appointment { get; set; }
        public ObservableCollection<TourGuest> Guests { get; set; } = new();

        private TourGuest _selectedGuest = new();
        public TourGuest SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                if (value != _selectedGuest)
                {
                    _selectedGuest = value;
                    OnPropertyChanged(nameof(SelectedGuest));
                }
            }
        }
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
                    Appointment.TourAppointment = _selectedAppointment;
                    OnPropertyChanged(nameof(SelectedAppointment));
                }
            }
        }
        public string KeyPoints { get; set; } = string.Empty;

        public AppointmentsViewModel(Tour tour)
        {
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _voucherService = Injector.GetService<VoucherSevice>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            _tourService = Injector.GetService<TourService>();

            Appointments = new(_tourAppointmentService.GetTodays(tour));
            Tour = new(tour);
            Appointment = new();
            KeyPoints = Tour.KeyPointsToString();
        }

        public void GetAllAppointments()
        {
            Appointments.Clear();
            Appointments = new(_tourAppointmentService.GetAllByTourId(Tour.Id));
        }
        public void AddGuests()
        {
            Guests = new(_tourGuestService.GetGuests(SelectedAppointment));
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

        public void GoNext()
        {
            if (Tour.Tour.KeyPoints.Last().Id == Appointment.CurrentKeyPointId)
            {
                MessageBox.Show("Došli ste do kraja, završite turu!");
                return;
            }

            KeyPoint Next = _tourService.GetNextKeyPoint(Appointment.TourAppointment);
            Appointment.TourAppointment = _tourAppointmentService.AdvanceNext(Appointment.Id, Next);
        }

        public void EndAppointment()
        {
            _tourAppointmentService.StopLiveTracking(Appointment.Id);
            MessageBox.Show("Tura završena.");
        }

        public void SignUpGuest()
        {
                _tourGuestService.SignUpGuest(SelectedGuest.GuestId, Appointment.Id);
                MessageBox.Show("Gost prijavljen!");
        }
    }
}
