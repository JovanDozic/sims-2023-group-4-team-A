using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
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
            MessageBoxResult result = MessageBox.Show("Da li ste sigurni da želite da otkažete rezervaciju?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _accommodationReservationViewModel.CancelReservation();
                _accommodationReservationViewModel.Update();
                _accommodationReservationViewModel.SendNotification();
                NavigationService.Navigate(new ReservationList(_user));
            }
        }

        private void Button_Click_Reschedule(object sender, RoutedEventArgs e)
        {
            var movingReservationsPage = new MovingReservation(_accommodationReservationViewModel.SelectedReservation, _user);
            NavigationService.Navigate(movingReservationsPage);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoveButton.IsEnabled = !_accommodationReservationViewModel.IsNotSelected() && !_accommodationReservationViewModel.IsReservationInPast() && !_accommodationReservationViewModel.IsReservationOnStandBy();
            CancelButton.IsEnabled = !_accommodationReservationViewModel.IsNotSelected() && _accommodationReservationViewModel.IsDateValid();
            ReportButton.IsEnabled = !_accommodationReservationViewModel.IsNotSelected();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(_accommodationReservationViewModel.IsPDFGenerated())
            {
                MessageBox.Show("Izvestaj uspesno kreiran!");
                NavigationService.Navigate(new MainPage(_user));
            }else
                NavigationService.Navigate(new MainPage(_user));



        }
    }
 }

