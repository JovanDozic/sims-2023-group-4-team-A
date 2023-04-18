using System;
using System.Windows;
using System.Windows.Controls;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;

namespace SIMSProject.WPF.Views.Guest1
{
    public partial class MovingReservations : Window
    {
        private readonly User User = new();
        private readonly ReschedulingRequestViewModel _reschedulingRequestViewModel;

        public MovingReservations(AccommodationReservation accommodationReservation)
        {
            InitializeComponent();
            User = new();
            _reschedulingRequestViewModel = new(User, accommodationReservation);
            DataContext = _reschedulingRequestViewModel;            
            ShowNameAndDate();
        }

        public void ShowNameAndDate()
        {
            NameBlock.Text = _reschedulingRequestViewModel.DisplayName();
            DateBlock.Text = _reschedulingRequestViewModel.DisplayDate();
        }

        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (DateFrom.SelectedDate.HasValue && DateFrom.SelectedDate.Value != DateTime.MinValue)
            {
                DateTo.SelectedDate = DateFrom.SelectedDate.Value.AddDays(_reschedulingRequestViewModel.GetDaysNumber());
            }
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (DateTo.SelectedDate.HasValue && DateTo.SelectedDate.Value != DateTime.MinValue)
            {
                DateFrom.SelectedDate = DateTo.SelectedDate.Value.AddDays(-_reschedulingRequestViewModel.GetDaysNumber());
            }
        }

        private void DatePicker1_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.DisplayDateStart = DateTime.Today.AddDays(1);
            }
        }

        private void DatePicker2_Loaded(object sender, RoutedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            if (datePicker != null)
            {
                datePicker.DisplayDateStart = DateTime.Today.AddDays(_reschedulingRequestViewModel.GetDaysNumber() + 1);
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_SendRequest(object sender, RoutedEventArgs e)
        {
            _reschedulingRequestViewModel.SendRequest();
            MessageBox.Show("Zahtev uspešno poslat!");
            Close();
        }
    }
}
