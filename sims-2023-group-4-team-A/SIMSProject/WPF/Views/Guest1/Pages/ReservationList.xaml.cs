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
    /// Interaction logic for ReservationList.xaml
    /// </summary>
    public partial class ReservationList : Page
    {
        private readonly User _user;
        private readonly AccommodationReservationViewModel _accommodationReservationViewModel;
        public ReservationList(User user)
        {
            InitializeComponent();
            _user = user;
            _accommodationReservationViewModel = new(_user);
            DataContext = _accommodationReservationViewModel;

            }
            private void Button_Click_Cancellation(object sender, RoutedEventArgs e)
            {
                if (!_accommodationReservationViewModel.IsDateValid())
                {
                    MessageBox.Show("Rezervaciju je moguće otkazati najkasnije 24h pre dolaska.");
                    return;
                }
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da otkažete rezervaciju?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    _accommodationReservationViewModel.CancelReservation();
                    _accommodationReservationViewModel.Update();
                    _accommodationReservationViewModel.SendNotification();
                    MessageBox.Show("Rezervacija je otkazana!");
                    NavigationService.Navigate(new ReservationList(_user));
                }
            }
          
            private void Button_Click_Reschedule(object sender, RoutedEventArgs e)
            {
                if (_accommodationReservationViewModel.IsReservationInPast())
                {
                    MessageBox.Show("Nije moguće pomeriti izabranu rezervaciju!");
                    return;
                }
                if (_accommodationReservationViewModel.IsReservationOnStandBy())
                {
                    MessageBox.Show("Zahtev za ovu rezervaciju je već poslat!");
                    return;
                }        
                 var movingReservationsPage = new MovingReservation(_accommodationReservationViewModel.SelectedReservation, _user);
                 NavigationService.Navigate(movingReservationsPage);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoveButton.IsEnabled = !_accommodationReservationViewModel.IsNotSelected();
            CancelButton.IsEnabled = !_accommodationReservationViewModel.IsNotSelected();
        }
    }
    }

