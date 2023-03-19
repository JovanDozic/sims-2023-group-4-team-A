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

        public ObservableCollection<KeyPoint> KeyPoints { get; set; }
        public ObservableCollection<Guest> AbsentGuests { get; set; }
        public ObservableCollection<Guest> PresentGuests { get; set; }
        public KeyPoint SelectedKeyPoint { get; set; }
        public TourDate SelectedDate { get; set; }




        public TourLiveTrackingWindow(TourDate Date)
        {
            InitializeComponent();
            this.DataContext = this;

            KeyPoints = new();
            AbsentGuests = new();
            PresentGuests = new();
            SelectedKeyPoint = new();
            SelectedDate = new();
            SelectedDate = Date;


            List<TourGuest> guests = tourGuestController.GetAll();

            foreach(var guest in Date.Guests)
            {
                if (guests.Find(x => x.GuestId == guest.Id).GuestStatus.Equals("Odsutan"))
                    AbsentGuests.Add(guest);
                else
                    PresentGuests.Add(guest);
            
            }

            Tour? tour = tourController.GetAll().Find(x => x.Id == Date.TourId);

            foreach(var key in tour.KeyPoints)
            {
                KeyPoints.Add(key);
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KeyPoint NextKeyPoint = tourController.FindNext(SelectedDate);
            if(NextKeyPoint.Id != SelectedKeyPoint.Id)
            {
                MessageBox.Show("Ne mozete preskakati kljucne tacke");
            }
            else
            {
                tourDateController.AdvanceToNext(SelectedDate.Id, SelectedKeyPoint);
            }

            
        }
    }
}
