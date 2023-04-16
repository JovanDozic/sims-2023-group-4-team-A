using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.OwnerViews
{
    public partial class ReschedulingRequestsView : Window
    {
        private readonly User _user;
        private readonly ReschedulingRequestViewModel _viewModel;
        private AccommodationReservation _accommodationReservation;

        public ReschedulingRequestsView(User user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new(_user, _accommodationReservation);
            DataContext = _viewModel;
        }

        private void SetButtonState(object sender, bool enabled)
        {
            if (sender is not Button button) return;
            button.IsEnabled = enabled;
            button.Visibility = enabled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DgrRequests_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.IsEnabled = _viewModel.IsRequestOnWaiting(e);
        }

        private void DgrRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgrRequests.SelectedItems.Count == 0) SetButtonState(BtnReviewRequest, false);
            else SetButtonState(BtnReviewRequest, true);
        }

        private void BtnReviewRequest_Click(object sender, RoutedEventArgs e)
        {
            if (DgrRequests.SelectedItems.Count == 0) return;

            DgrRequests.IsEnabled = false;
            SetButtonState(BtnAccept, true);
            SetButtonState(BtnDecline, true);
            SetButtonState(sender, false);

            DisplayAviabilityLabel(_viewModel.IsDateRangeAvailable());
        }

        private void DisplayAviabilityLabel(bool available)
        {
            LblDatesAvailable.Visibility = available ? Visibility.Visible : Visibility.Hidden;
            LblDatesUnavailable.Visibility = !available ? Visibility.Visible : Visibility.Hidden;
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni?", "Odobrenje zahteva", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            _viewModel.AcceptRequest();
            Close();
        }

        private void BtnDecline_Click(object sender, RoutedEventArgs e)
        {
            TbOwnerComment.IsEnabled = true;
            TbOwnerComment.Visibility = Visibility.Visible;
            SetButtonState(BtnSendDecline, true);
            SetButtonState(BtnSendDeclineCancel, true);
        }

        private void BtnSendDecline_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni?", "Odbijanje zahteva", MessageBoxButton.YesNo) == MessageBoxResult.No) return;
            _viewModel.RejectRequest();
            Close();
        }

        private void BtnSendDeclineCancel_Click(object sender, RoutedEventArgs e)
        {
            TbOwnerComment.IsEnabled = false;
            TbOwnerComment.Visibility = Visibility.Hidden;
            SetButtonState(BtnSendDecline, false);
            SetButtonState(BtnSendDeclineCancel, false);
        }

    }
}
