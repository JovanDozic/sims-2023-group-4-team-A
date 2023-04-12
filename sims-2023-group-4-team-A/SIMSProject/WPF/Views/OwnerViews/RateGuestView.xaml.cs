using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class RateGuestView : Window, INotifyPropertyChanged
    {
        private User _user;
        private readonly GuestRatingViewModel _viewModel;

        public GuestRating GuestRating { get; set; } = new();

        public RateGuestView(User user, AccommodationReservation reservation)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, reservation);
            DataContext = _viewModel;
        }

        private void BTNCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BTNRateGuest_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LeaveGuestRating();
            MessageBox.Show("Ocena uspešno ostavljena!", "Gost ocenjen", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}