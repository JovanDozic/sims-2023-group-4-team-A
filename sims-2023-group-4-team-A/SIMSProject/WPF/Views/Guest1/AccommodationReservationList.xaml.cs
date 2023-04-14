using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using SIMSProject.Model;
using SIMSProject.Observer;
using SIMSProject.Controller;
using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.WPF.Views.Guest1;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using SIMSProject.Domain.Models.UserModels;

namespace SIMSProject.WPF.Views.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationList.xaml
    /// </summary>
    public partial class AccommodationReservationList : Window
    {
        private readonly User _user;
        private readonly AccommodationReservationViewModel _accommodationReservationViewModel;
        public AccommodationReservation SelectedReservation { get; set; } = null;
        public CancelledReservationsNotifications CancelledReservationsNotifications { get; set; }
        public CancelledReservationsNotificationsController CancelledReservationsNotificationsController { get; set; }
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        private AccommodationReservationController AccommodationReservationController { get; set; }
        public AccommodationReservationList()
        {
            InitializeComponent();

            _accommodationReservationViewModel = new(_user);
            DataContext = _accommodationReservationViewModel;
            
        }

        private void Button_Click_Cancellation(object sender, RoutedEventArgs e)
        {
           
           
            if (_accommodationReservationViewModel.IsSelected())
            {
                if(_accommodationReservationViewModel.IsDateValid())
                {
                    MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da otkazete rezervaciju?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _accommodationReservationViewModel.CancelReservation();
                        _accommodationReservationViewModel.Update();
                        var message = _accommodationReservationViewModel.GetMessage();
                        //TODO Notifikacija
                        MessageBox.Show("Rezervacija je otkazana!");
                        Close();
                    }    
                }
                else
                {
                    MessageBox.Show("Rezervaciju je moguće bilo izvršiti najkasnije 24h pre dolaska");
                }
            }
            else
                MessageBox.Show("Morate da odaberete rezervaciju!");
            
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                var window = new MovingReservations(SelectedReservation);
                window.Show();
            }
            else
                MessageBox.Show("Morate da odaberete rezervaciju!");
            
        }
    }
}
