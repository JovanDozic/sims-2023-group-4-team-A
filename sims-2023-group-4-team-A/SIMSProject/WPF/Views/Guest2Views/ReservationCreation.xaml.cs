using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
using System.Windows;
using System.Windows.Controls;
using SIMSProject.Domain.Models.UserModels;
using System.Collections.ObjectModel;
using SIMSProject.WPF.ViewModels.TourViewModels;
using SIMSProject.Application.Services.TourServices;
using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SIMSProject.WPF.Converters;
using System.Windows.Data;

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for ReservationCreation.xaml
    /// </summary>
    public partial class ReservationCreation : Page, INotifyPropertyChanged
    {

        public Domain.Models.UserModels.Guest2 User = new();
        public TourGuest TourGuest = new();
        public Tour Tour { get; set; }
        public bool IsSuperGuide { get => Tour.IsSuperGuide; }
        public Tour AlternativeTour { get; set; }
        public TourAppointment AlternativeTourDate { get; set; } = new();
        public TourAppointment SelectedAppointment { get; set; }
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
        private string _notEnoughSpots = string.Empty;
        public string NotEnoughSpots
        {
            get => _notEnoughSpots;
            set
            {
                if (_notEnoughSpots != value)
                {
                    _notEnoughSpots = value;
                    OnPropertyChanged(nameof(NotEnoughSpots));
                }
            }
        }

        public ObservableCollection<Tour> AlternativeTours { get; set; }
        public ObservableCollection<TourAppointment> CBTourDates { get; set; } = new();
        public ObservableCollection<Voucher> CBVoucherDates { get; set; } = new();

        private VouchersViewModel _vouchersViewModel { get; set; }
        private TourAppointmentsViewModel _tourAppointmentsViewModel { get; set; }

        ////////////SERVISI
        private readonly TourReservationService _tourReservationService;
        private readonly TourAppointmentService _tourAppointmentService;
        private readonly LocationService _locationService;
        private readonly TourService _tourService;
        private readonly TourGuestService _tourGuestService;
        private readonly VoucherService _voucherService;

        public ReservationCreation(Domain.Models.UserModels.Guest2 user, Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Tour = selectedTour;
            _tourAppointmentsViewModel = new(selectedTour);
            //DataContext = _tourAppointmentsViewModel;
            _vouchersViewModel = new(user);

            _tourReservationService = Injector.GetService<TourReservationService>();
            _tourAppointmentService = Injector.GetService<TourAppointmentService>();
            _locationService = Injector.GetService<LocationService>();
            _tourService = Injector.GetService<TourService>();
            _tourGuestService = Injector.GetService<TourGuestService>();
            _voucherService = Injector.GetService<VoucherService>();

            _voucherService.WinVoucher(user.Id);
            AlternativeTours = new ObservableCollection<Tour>(_tourService.GetToursWithSameLocation(Tour));
            List<Location> tourLocations = _locationService.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.Location.Id);
            }

            CBTourDates = _tourAppointmentsViewModel.GetAllInactiveAppointments(Tour);
            CBVoucherDates = new ObservableCollection<Voucher>(_vouchersViewModel.Vouchers);
            ShowDetails(Tour);

            TextBlock tbKeyPoints = (TextBlock)FindName("TBKeyPoints");
            if (tbKeyPoints != null)
            {
                Binding binding = new Binding("Tour.KeyPoints");
                binding.Converter = (KeyPointsListToStringConverter)FindResource("KeyPointsListToStringConverter");
                tbKeyPoints.SetBinding(TextBlock.TextProperty, binding);
            }

            
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
            //TBLanguage.Text = tour.TourLanguage.ToString();
            TBMaxGuests.Text = tour.MaxGuestNumber.ToString();
            //dgrImageURLs.ItemsSource = tour.Images;
        }
        private void ReserveTour(TourReservation tourReservation, TourAppointment tourDate, int guestsForReservation)
        {
            tourReservation.TourAppointment.Id = tourDate.Id;
            tourReservation.GuestId = User.Id;
            tourReservation.GuestNumber = guestsForReservation;
            _tourReservationService.Save(tourReservation);
            tourDate.AvailableSpots -= guestsForReservation;
            _tourAppointmentService.UpdateAvailableSpots(tourDate);
            return;
        }

        private void MakeTourGuest(TourGuest tourGuest, TourAppointment tourAppointment)
        {
            TourGuest.TourAppointment.Id = tourAppointment.Id;
            tourGuest.Guest.Id = User.Id;
            tourGuest.JoiningPoint.Id = -1;
            _tourGuestService.Save(tourGuest);
            return;
        }
        private void UseVoucher()
        {
            if (SelectedVoucher == null) return;
            SelectedVoucher.Used = true;
            _vouchersViewModel.Update(SelectedVoucher);
            NewTourReservation.VoucherUsed = true;
            _tourReservationService.Update(NewTourReservation);
        }
        private void Reservation_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ShowAndSearchTours(User, Tour.Location.ToString()));
            if (SelectedAppointment == null) { LblPickAppointment.Visibility = Visibility.Visible; return; }
            if (SelectedAppointment.AvailableSpots == 0)
            {
                bool isAvailable = CheckAvailabilityAndShowAlternatives(AlternativeTour, AlternativeTourDate);
                //NavigationService.Navigate(new ShowAndSearchTours(User, Tour.Location.ToString()));

                if (!isAvailable) return;
            }

            bool spotsAvailable = CheckAvailableSpots(SelectedAppointment, GuestsForReservation);
            if (!spotsAvailable) return;
            ReserveTour(NewTourReservation, SelectedAppointment, GuestsForReservation);
            UseVoucher();
            MakeTourGuest(TourGuest, SelectedAppointment);
            LblReservationCreated.Visibility = Visibility.Visible;
            LblPickAppointment.Visibility = Visibility.Hidden;
            LblNotEnoughSpots.Visibility = Visibility.Hidden;
        }
        private bool CheckAvailabilityAndShowAlternatives(Tour alternativeTour, TourAppointment alternativeTourDate)
        {
            if (alternativeTour == null)
            {
                //MessageBox.Show("Na odabranoj turi nema vise slobodnih mesta. \nIzaberite drugi datum, neku od ponudjenih alternativnih tura ili odustanite. \n");
                return false;
            }
            if (GuestsForReservation > alternativeTourDate.AvailableSpots)
            {
                //MessageBox.Show("Nema dovoljno slobodnih mesta na turi.\nNa turi ima " + alternativeTourDate.AvailableSpots + " mesta.\n" +
                  //  "Izaberite neku od alternativnih tura ili promenite broj gostiju.");
                return false;
            }
            ReserveTour(NewTourReservation, alternativeTourDate, GuestsForReservation);
            UseVoucher();
            MakeTourGuest(TourGuest, alternativeTourDate);
            //MessageBox.Show("Rezervacija za " + GuestsForReservation + " osoba uspesna!");
            //Close();
            return true;
        }

        private bool CheckAvailableSpots(TourAppointment selectedAppointment, int guestsForReservation)
        {
            if (guestsForReservation > selectedAppointment.AvailableSpots)
            {
                NotEnoughSpots = "Na turi ima " + selectedAppointment.AvailableSpots + " mesta, izmenite zahtev!";
                LblNotEnoughSpots.Visibility = Visibility.Visible;
                LblPickAppointment.Visibility = Visibility.Hidden;
                return false;
            }
            return true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AlternativeGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Tour = AlternativeTour;
            ShowDetails(AlternativeTour);

            AlternativeTours = new ObservableCollection<Tour>(_tourService.GetToursWithSameLocation(AlternativeTour));
            List<Location> tourLocations = _locationService.GetAll();

            foreach (var tour in AlternativeTours)
            {
                tour.Location = tourLocations.Find(x => x.Id == tour.Location.Id);
            }
            CBSelectedTourDates.ItemsSource = _tourAppointmentsViewModel.GetAllInactiveAppointments(AlternativeTour);
        }

    }
}
