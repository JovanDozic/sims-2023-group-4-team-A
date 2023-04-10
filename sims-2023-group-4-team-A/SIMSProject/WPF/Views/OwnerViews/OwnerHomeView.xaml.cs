using System.Windows;
using SIMSProject.Controller;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.View.OwnerViews;
using SIMSProject.WPF.ViewModels.OwnerViewModels;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class OwnerHomeView : Window
    {
        private Owner _owner { get; set; }
        private OwnerHomeViewModel _viewModel { get; set; }

        public OwnerHomeView(Owner owner)
        {
            InitializeComponent();
            _owner = owner;
            _viewModel = new(_owner);
            DataContext = _viewModel;

            //CheckUnratedGuests();
        }



        //private void CheckUnratedGuests()
        //{
        //    foreach (var reservation in _reservationController.GetAll())
        //    {
        //        if (reservation.GuestRated || reservation.Accommodation.Owner.Id != _owner.Id)  continue;
        //        if (DateTime.Now < reservation.EndDate || DateTime.Now > reservation.EndDate.AddDays(5)) continue;
        //        if (!RateGuestDialogue(reservation)) continue;

        //        RateGuest window = new(_owner, reservation);
        //        window.ShowDialog();
        //        BtnRateGuest.IsEnabled = false;
        //    }

        //    new GuestController().RefreshRatings();
        //    _reservationController = new AccommodationReservationController();
        //    _owner.Accommodations = new AccommodationController().GetAllByOwner(_owner.Id);
        //    DgrReservations.Items.Refresh();
        //}

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
            RegisterAccommodation window = new(_owner);
            window.ShowDialog();
            RefreshAccommodations();
        }

        private void RefreshAccommodations()
        {
            _owner.Accommodations = new AccommodationController().GetAllByOwner(_owner.Id);
            DgrAccommodations.Items.Refresh();
        }

        //private void OpenRateGuestWindowButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DateTime.Now < SelectedReservation.EndDate) return;

        //    RateGuest window = new(_owner, SelectedReservation);
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
            _owner = new Owner(0, "<null>", "<null>");
            SignInView window = new();
            window.Show();
            Close();
        }

        private void BtnReschedulingRequests_Click(object sender, RoutedEventArgs e)
        {
            ReviewReschedulingRequests window = new(_owner);
            window.ShowDialog();
            RefreshAccommodations();
        }

    }
}