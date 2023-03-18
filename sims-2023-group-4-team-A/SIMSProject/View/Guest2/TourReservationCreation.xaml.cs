using SIMSProject.Controller;
using SIMSProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
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

namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for TourReservationCreation.xaml
    /// </summary>
    public partial class TourReservationCreation : Window, INotifyPropertyChanged
    {
        public Tour Tour { get; set; }
        public Tour AlternativeTour { get; set; } = null;
        public TourReservation NewTourReservation { get; set; } = new();
        private int _guestsForReservation = 1;
        public int GuestsForReservation
        {
            get => _guestsForReservation;
            set
            {
                if(value!=_guestsForReservation && value >= 1)
                {
                    _guestsForReservation = value;
                    OnPropertyChanged(nameof(GuestsForReservation));
                }
            }
        }
        
        private TourReservationController TourReservationController = new();
        private TourController TourController = new();
        private LocationController TourLocationController = new();
        private TourDateController TourDateController = new();

        public ObservableCollection<Tour> AlternativeTours { get; set; }
        public List<TourDate> TourDates { get; set; } = new();
        public TourDate SelectedTourDate { get; set; }

        public TourReservationCreation(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            if(AlternativeTour == null)
            {
                this.Tour = selectedTour;
            }
            else
            {
                this.Tour = AlternativeTour;
            }
            
            TBName.Text = Tour.Name;
            TBLocation.Text = Tour.Location.ToString();
            TBDescription.Text = Tour.Description;
            foreach (KeyPoint keyPoint in Tour.KeyPoints)
            {
                TBKeyPoints.Text += keyPoint.Description + "\n";
            }
            TBDuration.Text = Tour.Duration.ToString();
            TBLanguage.Text = Tour.TourLanguage;
            TBMaxGuests.Text = Tour.MaxGuestNumber.ToString();
            TBAvailableSpots.Text = Tour.AvailableSpots.ToString();

            AlternativeTours = new ObservableCollection<Tour>(TourController.GetToursWithSameLocation(selectedTour));
            List<Location> tourLocations = TourLocationController.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            }

            TourDates = TourDateController.GetAllByTourId(Tour.Id);

        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            GuestsForReservation = NumGuests.Value;
            if(Tour.AvailableSpots == 0)
            {
                MessageBox.Show("Na odabranoj turi nema vise slobodnih mesta. \n Izaberite neku od ponudjenih alternativnih tura ili odustanite. \n");
                AlternativeGrid.Visibility = Visibility.Visible;
                return;
            }else if (GuestsForReservation > Tour.AvailableSpots)
            {
                MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + Tour.AvailableSpots + " mesta.\n" +
                    "Izaberite neku od alternativnih tura ili promenite broj gostiju.");
                AlternativeGrid.Visibility = Visibility.Visible; 
                return;
            }
            else
            {
                NewTourReservation.TourDateId = SelectedTourDate.Id;
                NewTourReservation.GuestNumber = GuestsForReservation;
                Tour.AvailableSpots -= GuestsForReservation; 
                return;
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
