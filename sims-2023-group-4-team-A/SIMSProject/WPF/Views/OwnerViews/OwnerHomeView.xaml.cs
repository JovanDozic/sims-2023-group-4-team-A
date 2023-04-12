using System.Windows;
using System.Windows.Controls;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.OwnerViews;
using SIMSProject.WPF.ViewModels.OwnerViewModels;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerHomeView : Window
    {
        private User _user { get; set; }
        private OwnerHomeViewModel _viewModel { get; set; }

        public OwnerHomeView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user);
            DataContext = _viewModel;

            // TODO: call unrated guest check
        }

        //private void CheckUnratedGuests()
        //{
        //    foreach (var reservation in _reservationController.GetAll())
        //    {
        //        if (reservation.GuestRated || reservation.Accommodation._user.Id != _user.Id)  continue;
        //        if (DateTime.Now < reservation.EndDate || DateTime.Now > reservation.EndDate.AddDays(5)) continue;
        //        if (!RateGuestDialogue(reservation)) continue;
        //        RateGuestView window = new(_user, reservation);
        //        window.ShowDialog();
        //        BtnRateGuest.IsEnabled = false;
        //    }
        //    new GuestController().RefreshRatings();
        //    _reservationController = new AccommodationReservationController();
        //    _user.Accommodations = new AccommodationController().GetAllByOwner(_user.Id);
        //    DgrReservations.Items.Refresh();
        //}
        //private bool RateGuestDialogue(AccommodationReservation reservation)
        //{
        //    var message = "Gost <" + reservation.Guest.Username + "> koji je izašao iz " +
        //                  reservation.Accommodation.Name + " dana " + reservation.EndDate.ToString("dd.MM.yyyy") +
        //                  " nije ocenjen. Da li želite da ostavite ocenu?";
        //    return MessageBox.Show(message, "Ocenite gosta", MessageBoxButton.YesNo, MessageBoxImage.Question) ==
        //           MessageBoxResult.Yes;
        //}
        //private void OpenRegisterAccommodationWindowButton_Click(object sender, RoutedEventArgs e)
        //{
        //    RegisterAccommodationView window = new(_user);
        //    window.ShowDialog();
        //}
        //private void OpenRateGuestWindowButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DateTime.Now < SelectedReservation.EndDate) return;
        //    RateGuestView window = new(_user, SelectedReservation);
        //    window.Show();
        //    BtnRateGuest.IsEnabled = false;
        //    DgrReservations.Items.Refresh();
        //}
        //private void DGRReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (SelectedReservation.GuestRated || DateTime.Now < SelectedReservation.EndDate ||
        //        DateTime.Now > SelectedReservation.EndDate.AddDays(5))
        //    {
        //        BtnRateGuest.IsEnabled = false;
        //    }
        //    else
        //    {
        //        BtnRateGuest.IsEnabled = true;
        //    }
        //}

        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            _user = new Owner(0, "<null>", "<null>");
            SignInView window = new();
            window.Show();
            Close();
        }

        private void BtnReschedulingRequests_Click(object sender, RoutedEventArgs e)
        {
            //ReviewReschedulingRequests window = new(_user);
            //window.ShowDialog();
        }

        private void DgrAccommodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.LoadReservations();
            DgrReservations.Items.Refresh();
        }

        private void DgrReservations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BtnRateGuest.IsEnabled = _viewModel.IsGuestRatingEnabled();
        }

        private void BtnRegisterAccommodation_Click(object sender, RoutedEventArgs e)
        {
            RegisterAccommodationView window = new(_user);
            window.ShowDialog();
            _viewModel.LoadAccommodations();
            DgrAccommodations.Items.Refresh();
        }

        private void BtnRateGuest_Click(object sender, RoutedEventArgs e)
        {
            RateGuestView window = new(_user, _viewModel.SelectedReservation);
            window.ShowDialog();
            BtnRateGuest.IsEnabled = false;
            _viewModel.LoadReservations();
            DgrReservations.Items.Refresh();
        }
    }
}