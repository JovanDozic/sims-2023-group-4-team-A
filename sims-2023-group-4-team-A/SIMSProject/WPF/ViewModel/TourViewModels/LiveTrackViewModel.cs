using SIMSProject.Application1.Services.TourServices;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SIMSProject.WPF.ViewModel.TourViewModels
{
    public class LiveTrackViewModel
    {
        private readonly TourService _tourService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly TourGuestService _tourGuestService;

        public BaseTourViewModel Tour { get; set; }
        public TourAppointmentViewModel Appointment { get; set; }
        public ObservableCollection<TourGuest> Guests { get; set; } = new();
        public TourGuest SelectedGuest {get; set;} = new();
        public string KeyPoints { get; set; } = string.Empty;

        public LiveTrackViewModel(Tour tour, TourAppointment appointment)
        {
            _tourService = new();
            _tourAppointmentService = new();
            _tourGuestService = new();

            Tour = new(tour);
            Appointment = new(appointment);
            KeyPoints = Tour.KeyPointsToString();
        }

        private void RefreshGuests()
        {
            Guests.Clear();
            List<TourGuest> guests = _tourGuestService.GetGuests(Appointment.TourAppointment);

            foreach (var guest in guests)
            {
                Guests.Add(guest);
            }
        }

        public void GoNext()
        {
            if(Tour.GetTour().KeyPoints.Last().Id == Appointment.CurrentKeyPointId)
            {
                MessageBox.Show("Došli ste do kraja, završite turu!");
                return;
            }

            KeyPoint Next = _tourService.GoToNextKeyPoint(Appointment.GetAppointment());
            Appointment.TourAppointment = _tourAppointmentService.GoToNextKeyPoint(Appointment.Id, Next);
        }

        public void EndAppointment()
        {
            _tourService.EndTourAppointment(Tour.Id, Appointment.Id);
            _tourAppointmentService.StopLiveTracking(Appointment.Id);
            MessageBox.Show("Tura završena.");
        }

        public void SignUpGuest()
        {
            _tourGuestService.SignUpGuest(SelectedGuest.GuestId, Appointment.Id);
            RefreshGuests();
            MessageBox.Show("Gost prijavljen!");
        }



    }
}
