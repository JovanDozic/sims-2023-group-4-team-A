using SIMSProject.Controller;
using SIMSProject.Model;
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
        public ObservableCollection<TourDate> AlternativeTourDates { get; set; }
        public List<TourDate> TourDates { get; set; } = new();
        public List<TourDate> CBTourDates { get; set; } = new();
        public TourDate SelectedTourDate { get; set; }

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
            //foreach (Tour tour in AlternativeTours)
            //{
            //    TourDates = TourDateController.GetAllByTourId(tour.Id);
            //}
            //AlternativeTourDates = new ObservableCollection<TourDate>(TourDates);

            List<Location> tourLocations = TourLocationController.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            }

            CBTourDates = TourDateController.GetAllByTourId(Tour.Id);
            

        }

        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            GuestsForReservation = NumGuests.Value;
            if(Tour.AvailableSpots == 0)
            {
                MessageBox.Show("Na odabranoj turi nema vise slobodnih mesta. \n Izaberite neku od ponudjenih alternativnih tura ili odustanite. \n");
                AlternativeGrid.Visibility = Visibility.Visible;
                if (AlternativeTour != null)
                {
                    if(GuestsForReservation > AlternativeTour.AvailableSpots)
                    {
                        MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + AlternativeTour.AvailableSpots + " mesta.\n");
                    }
                    else
                    {
                        NewTourReservation.TourDateId = SelectedTourDate.Id;
                        NewTourReservation.GuestNumber = GuestsForReservation;
                        NewTourReservation.GuestId = User.Id;
                        TourReservationController.Create(NewTourReservation);
                        AlternativeTour.AvailableSpots -= GuestsForReservation; //u tour date ce se smanjivati
                        MessageBox.Show("Rezervacija uspesna!");
                        return;
                    }
                }
                return;
            }else if (GuestsForReservation > Tour.AvailableSpots)
            {
                MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + Tour.AvailableSpots + " mesta.\n" +
                    "Izaberite neku od alternativnih tura ili promenite broj gostiju.");
                //AlternativeGrid.Visibility = Visibility.Visible; 
                return;
            }
            else
            {
                NewTourReservation.TourDateId = SelectedTourDate.Id;
                NewTourReservation.GuestNumber = GuestsForReservation;
                NewTourReservation.GuestId = User.Id;
                TourReservationController.Create(NewTourReservation);
                Tour.AvailableSpots -= GuestsForReservation; //u tour date ce se smanjivati
                MessageBox.Show("Rezervacija uspesna!");
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

            CBTourDates = TourDateController.GetAllByTourId(AlternativeTour.Id);
        }
    }
}
