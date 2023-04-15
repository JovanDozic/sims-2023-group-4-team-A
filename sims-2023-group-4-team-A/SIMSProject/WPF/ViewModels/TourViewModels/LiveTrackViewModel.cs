using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModels.TourViewModels
{
    public class LiveTrackViewModel: ViewModelBase
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourGuestService _tourGuestService;

        public BaseTourViewModel Tour { get; set; }
        public BaseAppointmentViewModel Appointment { get; set; }
        public ObservableCollection<TourGuest> Guests { get; set; } = new();

        private TourGuest _selectedGuest = new();
        public TourGuest SelectedGuest
        {
            get => _selectedGuest;
            set
            {
                if(value != _selectedGuest)
                {
                    _selectedGuest = value;
                    OnPropertyChanged(nameof(SelectedGuest));
                }
            }
        }
        public string KeyPoints { get; set; } = string.Empty;

        public LiveTrackViewModel(Tour tour, TourAppointment appointment)
        {
            _tourService = Injector.GetService<TourService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _tourGuestService = Injector.GetService<TourGuestService>();

            Tour = new(tour);
            Appointment = new(appointment);
            KeyPoints = Tour.KeyPointsToString();
            Guests = new(_tourGuestService.GetGuests(appointment));

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
