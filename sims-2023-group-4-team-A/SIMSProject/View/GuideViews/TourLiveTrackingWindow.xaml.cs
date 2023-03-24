using SIMSProject.Controller;
using SIMSProject.CustomControls;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourLiveTrackingWindow.xaml
    /// </summary>
    public partial class TourLiveTrackingWindow : Window
    {
        private readonly TourController tourController = new();
        private readonly TourAppointmentController tourDateController = new();
        private readonly TourGuestController tourGuestController = new();
        private readonly KeyPoint lastKeyPoint = new();

        public ObservableCollection<KeyPoint> KeyPoints { get; set; } = new();
        public ObservableCollection<Guest> Guests { get; set; } = new();
        public KeyPoint CurrentKeyPoint { get; set; } = new();
        public TourAppointment CurrentAppointment { get; set; } = new();
        public Guest PendingGuest { get; set; } = new();




        public TourLiveTrackingWindow(TourAppointment appointment)
        {
            InitializeComponent();
            this.DataContext = this;

            CurrentAppointment = tourDateController.GetById(appointment.Id);
            CurrentKeyPoint = CurrentAppointment.CurrentKeyPoint;
            lastKeyPoint = tourController.GetLast(CurrentAppointment);
            CurrentKeyPointTB.Text = CurrentKeyPoint.ToString();
            AddGuests();
            AddKeyPoints();
        }

        private void AddKeyPoints()
        {
            Tour? tour = tourController.GetAll().Find(x => x.Id == CurrentAppointment.TourId) ?? throw new Exception("Greška. Tura ne postoji!");

            foreach (var key in tour.KeyPoints)
            {
                KeyPoints.Add(key);
            }
        }

        private void AddGuests()
        {
            Guests.Clear();
            List<TourGuest> guests = tourGuestController.GetAll();

            foreach (var guest in CurrentAppointment.Guests)
            {
                TourGuest? tourGuest = guests.Find(x => x.GuestId == guest.Id);
                if (tourGuest == null) continue;
                Guests.Add(guest);
                
            }
        }

        private void Go_nextBTN_Click(object sender, RoutedEventArgs e)
        {
            AddGuests();
            
            if(lastKeyPoint.Equals(CurrentKeyPoint))
            {
                MessageBox.Show("Došli ste do kraja, završite turu!");
                return;
            }

            KeyPoint NextKeyPoint = tourController.GoToNextKeyPoint(CurrentAppointment);
            if(NextKeyPoint != null)
            {
                tourDateController.AdvanceToNext(CurrentAppointment.Id, NextKeyPoint);
                CurrentKeyPoint = NextKeyPoint;
                CurrentKeyPointTB.Text = CurrentKeyPoint.ToString();
            }
        }

        private void PauseBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            tourController.EndTour(CurrentAppointment.TourId, CurrentAppointment.Id);
            tourDateController.StopTourLiveTracking(CurrentAppointment.Id);
            MessageBox.Show("Tura završena.");
            Close();
        }

        private void Sign_guestBTN_Click(object sender, RoutedEventArgs e)
        {
            tourGuestController.SignGuest(PendingGuest.Id, CurrentAppointment.Id);
            MessageBox.Show("Gost prijavljen!");
        }
    }
}
