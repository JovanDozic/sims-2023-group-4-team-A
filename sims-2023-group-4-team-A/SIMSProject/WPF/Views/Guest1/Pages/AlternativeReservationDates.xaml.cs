using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.WPF.ViewModels.Guest1ViewModels;
using SIMSProject.WPF.Views.Guest1.MainView;
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
    /// Interaction logic for AlternativeReservationDates.xaml
    /// </summary>
    public partial class AlternativeReservationDates : Page
    {
        private readonly User _user = new();
        private ReservationViewModel _reservationViewModel;
        public AlternativeReservationDates(ReservationViewModel reservationViewModel)
        {
            InitializeComponent();
            _reservationViewModel = reservationViewModel;
            DataContext = _reservationViewModel;
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_Confirm(object sender, RoutedEventArgs e)
        {
            _reservationViewModel.SaveReservation();
            MessageBox.Show("Smeštaj uspešno rezervisan!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new MainPage(_user));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_reservationViewModel.IsSelected())
                ReservationButton.IsEnabled = true;
        }
    }
}
