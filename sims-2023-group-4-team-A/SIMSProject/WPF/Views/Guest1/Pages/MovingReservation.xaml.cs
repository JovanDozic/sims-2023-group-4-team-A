using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.Guest1.Pages
{
    /// <summary>
    /// Interaction logic for MovingReservation.xaml
    /// </summary>
    public partial class MovingReservation : Page
    {
        private readonly User _user = new();
        private readonly ReschedulingRequestViewModel _reschedulingRequestViewModel;
        public MovingReservation(AccommodationReservation accommodationReservation, User user)
        {
            InitializeComponent();
            _user = user;
            _reschedulingRequestViewModel = new(_user, accommodationReservation);
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
            NavigationService.GoBack();
        }

        private void Button_Click_SendRequest(object sender, RoutedEventArgs e)
        {
            _reschedulingRequestViewModel.SendRequest();
            MessageBox.Show("Zahtev uspešno poslat!");
            NavigationService.Navigate(new ReservationList(_user));
            //TODO zatvoriti prozor
        }
    }
}
