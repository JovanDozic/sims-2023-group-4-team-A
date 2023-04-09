using SIMSProject.Controller;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.View.OwnerViews
{
    public partial class ReviewReschedulingRequests : Window, INotifyPropertyChanged
    {
        public Owner User { get; set; } = new();

        public List<ReschedulingRequest> Requests { get; set; } = new();
        private ReschedulingRequestController _requestController { get; set; } = new();
        private AccommodationReservationController _reservationController { get; set; } = new();

        private ReschedulingRequest _selectedRequest = new();
        public ReschedulingRequest SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                if (value == _selectedRequest) return;
                _selectedRequest = value;
                OnPropertyChanged();
            }
        }

        public ReviewReschedulingRequests(Owner user)
        {
            InitializeComponent();
            DataContext = this;

            User = user;

            // Requests = _requestController.GetAllOnWaitByOwnerId(User.Id);
            Requests = _requestController.GetAllByOwnerId(User.Id);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetButtonState(object sender, bool enabled)
        {
            var button = sender as Button;
            if (button == null) return;

            if (enabled)
            {
                button.IsEnabled = true;
                button.Visibility = Visibility.Visible;
            }
            else
            {
                button.IsEnabled = false;
                button.Visibility = Visibility.Hidden;
            }
        }

        private void DisplayAviabilityLabel(bool available)
        {
            if (available)
            {
                LblDatesAvailable.Visibility = Visibility.Visible;
                LblDatesUnavailable.Visibility = Visibility.Hidden;
                return;
            }
            LblDatesAvailable.Visibility = Visibility.Hidden;
            LblDatesUnavailable.Visibility = Visibility.Visible;
        }

        private void BtnReviewRequest_Click(object sender, RoutedEventArgs e)
        {
            if (DgrRequests.SelectedItems.Count == 0) return;

            DgrRequests.IsEnabled = false;
            SetButtonState(BtnAccept, true);
            SetButtonState(BtnDecline, true);
            SetButtonState(sender, false);

            CheckAndDisplayAvailability();
        }

        private void CheckAndDisplayAvailability()
        {
            if (_reservationController.IsAvailable(SelectedRequest.AccommodationReservation, 
                new DateRange(SelectedRequest.NewStartDate, SelectedRequest.NewEndDate)))
                DisplayAviabilityLabel(true);
            else DisplayAviabilityLabel(false);
        }

        private void DgrRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgrRequests.SelectedItems.Count == 0) SetButtonState(BtnReviewRequest, false);
            else SetButtonState(BtnReviewRequest, true);
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni?", "Odobrenje zahteva", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                SelectedRequest.Status = "Odobren";
                SelectedRequest.AccommodationReservation.StartDate = SelectedRequest.NewStartDate;
                SelectedRequest.AccommodationReservation.EndDate = SelectedRequest.NewEndDate;
                _reservationController.Update(SelectedRequest.AccommodationReservation);
                _requestController.Update(SelectedRequest);
            }
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
            SelectedRequest.Status = "Odbijen";
            _requestController.Update(SelectedRequest);
            Close();
        }

        private void BtnSendDeclineCancel_Click(object sender, RoutedEventArgs e)
        {
            TbOwnerComment.IsEnabled = false;
            TbOwnerComment.Visibility = Visibility.Hidden;
            SetButtonState(BtnSendDecline, false);
            SetButtonState(BtnSendDeclineCancel, false);
        }

        private void DgrRequests_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (Requests[e.Row.GetIndex()].Status != "Na čekanju")
            {
                e.Row.IsEnabled = false;
            }
        }
    }
}
