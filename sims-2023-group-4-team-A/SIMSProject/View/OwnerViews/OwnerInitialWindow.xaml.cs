using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using SIMSProject.Controller;
using SIMSProject.Controller.UserController;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;

namespace SIMSProject.View.OwnerViews
{
    public partial class OwnerInitialWindow : Window, INotifyPropertyChanged
    {
        public Owner User { get; set; } = new();
        private Accommodation _selectedAccommodation = new();

        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value != _selectedAccommodation)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

        private AccommodationReservation _selectedReservation = new();

        public AccommodationReservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                if (value != _selectedReservation)
                {
                    _selectedReservation = value;
                    OnPropertyChanged();
                }
            }
        }

        private AccommodationReservationController _reservationController = new();

        public OwnerInitialWindow(Owner user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;

            CheckUnratedGuests();
        }

        private void CheckUnratedGuests()
        {
            foreach (var reservation in _reservationController.GetAll())
            {
                if (reservation.GuestRated || reservation.Accommodation.Owner.Id != User.Id)
                {
                    continue;
                }

                if (DateTime.Now < reservation.EndDate || DateTime.Now > reservation.EndDate.AddDays(5))
                {
                    continue;
                }

                if (RateGuestDialogue(reservation))
                {
                    RateGuest window = new(User, reservation);
                    window.ShowDialog();
                    BTNRateGuest.IsEnabled = false;
                }
            }

            new GuestController().RefreshRatings();
            _reservationController = new AccommodationReservationController();
            User.Accommodations = new AccommodationController().GetAllByOwner(User.Id);
            DGRReservations.Items.Refresh();
        }

        private bool RateGuestDialogue(AccommodationReservation reservation)
        {
            var message = "Gost <" + reservation.Guest.Username + "> koji je izašao iz " +
                          reservation.Accommodation.Name + " dana " + reservation.EndDate.ToString("dd.MM.yyyy") +
                          " nije ocenjen. Da li želite da ostavite ocenu?";

            return MessageBox.Show(message, "Ocenite gosta", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
                   MessageBoxResult.Yes;
        }

        private void OpenRegisterAccommodationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodation window = new();
            window.Show();
        }

        private void OpenRateGuestWindowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.Now < SelectedReservation.EndDate)
            {
                return;
            }

            RateGuest window = new(User, SelectedReservation);
            window.Show();
            BTNRateGuest.IsEnabled = false;
            DGRReservations.Items.Refresh();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void DGRReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedReservation.GuestRated || DateTime.Now < SelectedReservation.EndDate ||
                DateTime.Now > SelectedReservation.EndDate.AddDays(5))
            {
                BTNRateGuest.IsEnabled = false;
            }
            else
            {
                BTNRateGuest.IsEnabled = true;
            }
        }
    }
}