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
        private readonly TourDateController tourDateController = new();
        private readonly TourGuestController tourGuestController = new();


        private KeyPoint LastKeyPoint = new();

        public ObservableCollection<KeyPoint> KeyPoints { get; set; } = new();
        public ObservableCollection<Guest> AbsentGuests { get; set; } = new();
        public ObservableCollection<Guest> PresentGuests { get; set; } = new();
        public KeyPoint CurrentKeyPoint { get; set; } = new();
        public TourDate SelectedDate { get; set; } = new();
        public Guest PendingGuest { get; set; } = new();




        public TourLiveTrackingWindow(TourDate Date)
        {
            InitializeComponent();
            this.DataContext = this;

            SelectedDate = tourDateController.GetById(Date.Id);
            CurrentKeyPoint = SelectedDate.CurrentKeyPoint;
            LastKeyPoint = tourController.GetLast(SelectedDate);


            CurrentKeyPointTB.Text = CurrentKeyPoint.ToString();
            SortGuests();
            AddKeyPoints();
        }

        private void AddKeyPoints()
        {
            Tour? tour = tourController.GetAll().Find(x => x.Id == SelectedDate.TourId);
            if (tour == null) throw new Exception("Greška. Tura ne postoji!");

            foreach (var key in tour.KeyPoints)
            {
                KeyPoints.Add(key);
            }
        }

        private void SortGuests()
        {
            AbsentGuests.Clear();
            PresentGuests.Clear();

            List<TourGuest> guests = tourGuestController.GetAll();

            foreach (var guest in SelectedDate.Guests)
            {
                TourGuest? tourGuest = guests.Find(x => x.GuestId == guest.Id);
                if (tourGuest == null) continue;
                
                bool guestNotPresent = tourGuest.GuestStatus.Equals("Odsutan") || tourGuest.GuestStatus.Equals("Prijavljen");
                if (guestNotPresent)
                    AbsentGuests.Add(guest);
                else
                {
                    if (tourGuest.JoinedKeyPointId == -1)
                        tourGuestController.MakeGuestPresent(guest.Id, SelectedDate.Id, CurrentKeyPoint);

                    PresentGuests.Add(guest);
                }
            }
        }

        private void Go_nextBTN_Click(object sender, RoutedEventArgs e)
        {
            SortGuests();
            
            if(LastKeyPoint.Equals(CurrentKeyPoint))
            {
                MessageBox.Show("Došli ste do kraja, završite turu!");
                return;
            }

            KeyPoint NextKeyPoint = tourController.FindNext(SelectedDate);
            if(NextKeyPoint != null)
            {
                tourDateController.AdvanceToNext(SelectedDate.Id, NextKeyPoint);
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
            tourController.EndTour(SelectedDate.TourId, SelectedDate.Id);
            tourDateController.StopTourLiveTracking(SelectedDate.Id);
            MessageBox.Show("Tura završena.");
            Close();
        }

        private void Sign_guestBTN_Click(object sender, RoutedEventArgs e)
        {
            tourGuestController.SignGuest(PendingGuest.Id, SelectedDate.Id);
            MessageBox.Show("Gost prijavljen!");
        }
    }
}
