using SIMSProject.Controller;
using SIMSProject.Controller.UserController;
using SIMSProject.Model;
using SIMSProject.Model.DAO.UserModelDAO;
using SIMSProject.Model.UserModel;
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
        public Guest User = new();
        public Tour Tour { get; set; }
        public Tour AlternativeTour { get; set; }
        public TourDate AlternativeTourDate { get; set; } = new();
        public TourDate SelectedTourDate { get; set; }
        public TourReservation NewTourReservation { get; set; } = new();

        private int _guestsForReservation = 1;
        public int GuestsForReservation
        {
            get => _guestsForReservation;
            set
            {
                if (value != _guestsForReservation && value >= 1)
                {
                    _guestsForReservation = value;
                    OnPropertyChanged(nameof(GuestsForReservation));
                }
            }
        }

        public TourReservationController TourReservationController = new();
        public TourController TourController = new();
        public LocationController TourLocationController = new();
        public TourDateController TourDateController = new();
        public GuestController GuestController = new();

        public ObservableCollection<Tour> AlternativeTours { get; set; }
        public List<TourDate> CBTourDates { get; set; } = new();

        public TourReservationCreation(Guest user, Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Tour = selectedTour;

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

            AlternativeTours = new ObservableCollection<Tour>(TourController.GetToursWithSameLocation(Tour));
            List<Location> tourLocations = TourLocationController.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            }

            CBTourDates = TourDateController.GetAllByTourId(Tour.Id);
        }
        private void ReserveTour(TourReservation tourReservation, TourDate tourDate, int guestsForReservation)
        {
            tourReservation.TourDateId = tourDate.TourId;
            tourReservation.GuestId = User.Id;
            tourReservation.GuestNumber = guestsForReservation;
            TourReservationController.Create(tourReservation);
            tourDate.AvailableSpots -= guestsForReservation;
            TourDateController.UpdateAvailableSpots(tourDate);
            return;
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            GuestsForReservation = NumGuests.Value;
            if (SelectedTourDate == null) return;
            if (SelectedTourDate.AvailableSpots == 0)
            {
                MessageBox.Show("Na odabranoj turi nema vise slobodnih mesta. \nIzaberite neku od ponudjenih alternativnih tura ili odustanite. \n");
                LBLAlternativneTure.Visibility = Visibility.Visible;
                AlternativeGrid.Visibility = Visibility.Visible;
                if (AlternativeTour == null) return;
                if (GuestsForReservation > AlternativeTourDate.AvailableSpots)
                {
                    MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + AlternativeTourDate.AvailableSpots + " mesta.\n");
                    return;
                }
                else
                {
                    ReserveTour(NewTourReservation, AlternativeTourDate, GuestsForReservation);
                    MessageBox.Show("Rezervacija za " + GuestsForReservation + " osoba uspesna!");
                    return;
                }
            }
            else if (GuestsForReservation > SelectedTourDate.AvailableSpots)
            {
                MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + SelectedTourDate.AvailableSpots + " mesta.\n" +
                    "Izaberite neku od alternativnih tura ili promenite broj gostiju.");
                LBLAlternativneTure.Visibility = Visibility.Visible;
                AlternativeGrid.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                ReserveTour(NewTourReservation, SelectedTourDate, GuestsForReservation);
                MessageBox.Show("Rezervacija za " + GuestsForReservation + " osoba uspesna!");
                return;
            }

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AlternativeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TBName.Text = AlternativeTour.Name;
            TBLocation.Text = AlternativeTour.Location.ToString();
            TBDescription.Text = AlternativeTour.Description;
            foreach (KeyPoint keyPoint in AlternativeTour.KeyPoints)
            {
                TBKeyPoints.Text += keyPoint.Description + "\n";
            }
            TBDuration.Text = AlternativeTour.Duration.ToString();
            TBLanguage.Text = AlternativeTour.TourLanguage;
            TBMaxGuests.Text = AlternativeTour.MaxGuestNumber.ToString();

            AlternativeTours = new ObservableCollection<Tour>(TourController.GetToursWithSameLocation(AlternativeTour));
            List<Location> tourLocations = TourLocationController.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            }

            CBSelectedTourDates.ItemsSource = TourDateController.GetAllByTourId(AlternativeTour.Id);
        }
    }
}
