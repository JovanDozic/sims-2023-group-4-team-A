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
        }
        private void StartDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
             DateTo.SelectedDate = _reschedulingRequestViewModel.GetUpdatedEndDate(DateFrom.SelectedDate); 
        }

        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateFrom.SelectedDate = _reschedulingRequestViewModel.GetUpdatedStartDate(DateTo.SelectedDate);
        }

        private void DatePicker1_Loaded(object sender, RoutedEventArgs e)
        {
            _reschedulingRequestViewModel.LoadFirstDatePicker(sender);
        }

        private void DatePicker2_Loaded(object sender, RoutedEventArgs e)
        {
            _reschedulingRequestViewModel.LoadSecondDatePicker(sender);
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
