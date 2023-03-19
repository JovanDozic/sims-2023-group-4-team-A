using SIMSProject.Controller.UserController;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace SIMSProject.View.OwnerViews
{
    /// <summary>
    /// Interaction logic for OwnerInitialWindow.xaml
    /// </summary>
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
                    OnPropertyChanged(nameof(SelectedAccommodation));
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
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }

        public OwnerInitialWindow(Owner user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;

            new GuestController().RefreshRatings();
        }

        private void OpenRegisterAccommodationWindowButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodation window = new();
            window.Show();
        }

        private void OpenRateGuestWindowButton_Click(object sender, RoutedEventArgs e)
        {
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

        private void DGRReservations_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SelectedReservation.GuestRated) BTNRateGuest.IsEnabled = false;
            else BTNRateGuest.IsEnabled = true;
        }
    }
}
