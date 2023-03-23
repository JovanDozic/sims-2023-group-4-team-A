using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMSProject.Controller;
using SIMSProject.Controller.UserController;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;

namespace SIMSProject.View.OwnerViews
{
    public partial class RateGuest : Window, INotifyPropertyChanged
    {
        public Owner User { get; set; } = new();
        public GuestRating GuestRating { get; set; } = new();
        private AccommodationReservation _reservation = new();

        public AccommodationReservation Reservation
        {
            get => _reservation;
            set
            {
                if (_reservation == value) return;
                _reservation = value;
                OnPropertyChanged();
            }
        }

        public RateGuest(Owner user, AccommodationReservation reservation)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Reservation = reservation;
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void BTNRateGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestRatingController controller = new();
            GuestRating.Reservation = Reservation;
            controller.Create(GuestRating);

            new GuestController().RefreshRatings();

            Reservation.GuestRated = true;
            var reservationController = new AccommodationReservationController();
            reservationController.UpdateExisting(Reservation);

            MessageBox.Show("Ocena uspešno ostavljena!", "Gost ocenjen", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}