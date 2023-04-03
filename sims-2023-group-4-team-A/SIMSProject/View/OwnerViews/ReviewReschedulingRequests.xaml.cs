using SIMSProject.Controller;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace SIMSProject.View.OwnerViews
{
    public partial class ReviewReschedulingRequests : Window, INotifyPropertyChanged
    {
        public Owner User { get; set; } = new();

        public List<ReschedulingRequest> Requests { get; set; } = new();
        private ReschedulingRequestController _requestController { get; set; } = new();
        private AccommodationController _accommodationController { get; set; } = new();

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

            DateTime newStartDate = SelectedRequest.NewStartDate;
            DateTime newEndDate = SelectedRequest.NewStartDate.AddDays(SelectedRequest.AccommodationReservation.NumberOfDays);

            if (_accommodationController.IsOccupied(SelectedRequest.AccommodationReservation.Accommodation.Id, newStartDate, newEndDate)) DisplayAviabilityLabel(false);
            else DisplayAviabilityLabel(true);

        }

        private void DgrRequests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DgrRequests.SelectedItems.Count == 0) SetButtonState(BtnReviewRequest, false);
            else SetButtonState(BtnReviewRequest, true);
        }

        private void BtnAccept_Click(object sender, RoutedEventArgs e)
        {

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
