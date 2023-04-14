using SIMSProject.Controller;
using SIMSProject.Controller.UserController;
using SIMSProject.Domain.Models;
using SIMSProject.Model;
using SIMSProject.Domain.Models.TourModels;
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
        public TourAppointment AlternativeTourDate { get; set; } = new();
        public TourAppointment   SelectedTourDate { get; set; }
        public TourReservation NewTourReservation { get; set; } = new();
        public Voucher SelectedVoucher { get; set; }

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

        private string _selectedImageFile = string.Empty;
        public string SelectedImageFile
        {
            get => _selectedImageFile;
            set
            {
                if (_selectedImageFile != value)
                {
                    _selectedImageFile = value;
                    OnPropertyChanged(nameof(SelectedImageFile));
                }
            }
        }

        public TourReservationController TourReservationController = new();
        public TourController TourController = new();
        public LocationController TourLocationController = new();
        public TourAppointmentController TourDateController = new();
        public VoucherController VoucherController = new();


        public ObservableCollection<Tour> AlternativeTours { get; set; }
        public List<TourAppointment> CBTourDates { get; set; } = new();
        public ObservableCollection<Voucher> CBVoucherDates { get; set; } = new();

        public TourReservationCreation(Guest user, Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Tour = selectedTour;
            ShowDetails(Tour);

            AlternativeTours = new ObservableCollection<Tour>(TourController.GetToursWithSameLocation(Tour));
            List<Location> tourLocations = TourLocationController.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            }

            CBTourDates = TourDateController.GetAllByTourId(Tour.Id);
            CBVoucherDates = new ObservableCollection<Voucher>(VoucherController.GetVouchersByGuestId(User.Id));

        }

        private void ShowDetails(Tour tour)
        {
            TBName.Text = tour.Name;
            TBLocation.Text = tour.Location.ToString();
            TBDescription.Text = tour.Description;
            foreach (KeyPoint keyPoint in tour.KeyPoints)
            {
                TBKeyPoints.Text += keyPoint.Description + "\n";
            }
            TBDuration.Text = tour.Duration.ToString();
            TBLanguage.Text = tour.TourLanguage;
            TBMaxGuests.Text = tour.MaxGuestNumber.ToString();
            dgrImageURLs.ItemsSource = tour.Images;
        }
        private void ReserveTour(TourReservation tourReservation, TourAppointment tourDate, int guestsForReservation)
        {
            tourReservation.TourDateId = tourDate.TourId;
            tourReservation.GuestId = User.Id;
            tourReservation.GuestNumber = guestsForReservation;
            TourReservationController.Create(tourReservation);
            tourDate.AvailableSpots -= guestsForReservation;
            TourDateController.UpdateAvailableSpots(tourDate);
            return;
        }

        private void UseVoucher()
        {
            if(CBSelectedVoucher != null)
            {
                VoucherController.Delete(SelectedVoucher);
            }

        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            GuestsForReservation = NumGuests.Value;
            if (SelectedTourDate == null) return;
            if (SelectedTourDate.AvailableSpots == 0)
            {
                
                LBLAlternativneTure.Visibility = Visibility.Visible;
                AlternativeGrid.Visibility = Visibility.Visible;
                if (AlternativeTour == null)
                {
                    MessageBox.Show("Na odabranoj turi nema vise slobodnih mesta. \nIzaberite drugi datum, neku od ponudjenih alternativnih tura ili odustanite. \n");
                    return;
                }
                    
                if (GuestsForReservation > AlternativeTourDate.AvailableSpots)
                {
                    MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + AlternativeTourDate.AvailableSpots + " mesta.\n" +
                    "Izaberite neku od alternativnih tura ili promenite broj gostiju.");
                    return;
                }
                else
                {
                    ReserveTour(NewTourReservation, AlternativeTourDate, GuestsForReservation);
                    MessageBox.Show("Rezervacija za " + GuestsForReservation + " osoba uspesna!");
                    Close();
                    return;
                }
            }else if (GuestsForReservation > SelectedTourDate.AvailableSpots)
            {
                MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + SelectedTourDate.AvailableSpots + " mesta.\n" +
                    "Izaberite drugi datum, neku od alternativnih tura ili promenite broj gostiju.");
                LBLAlternativneTure.Visibility = Visibility.Visible;
                AlternativeGrid.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                ReserveTour(NewTourReservation, SelectedTourDate, GuestsForReservation);
                UseVoucher();
                MessageBox.Show("Rezervacija za " + GuestsForReservation + " osoba uspesna!");
                Close();
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
            ShowDetails(AlternativeTour);

            AlternativeTours = new ObservableCollection<Tour>(TourController.GetToursWithSameLocation(AlternativeTour));
            List<Location> tourLocations = TourLocationController.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.LocationId);
            }
            CBSelectedTourDates.ItemsSource = TourDateController.GetAllByTourId(AlternativeTour.Id);
        }

        private void imagesGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TXTImagePlaceholder.Text = "Učitavanje...";
        }
    }
}
