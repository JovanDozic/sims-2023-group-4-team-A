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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationList.xaml
    /// </summary>
    public partial class AccommodationReservationList : Window
    {
        public AccommodationReservation SelectedReservation { get; set; } = null;
        public CancelledReservationsNotifications CancelledReservationsNotifications { get; set; }
        public CancelledReservationsNotificationsController CancelledReservationsNotificationsController { get; set; }
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        private AccommodationReservationController AccommodationReservationController { get; set; }
        public AccommodationReservationList()
        {
            InitializeComponent();
            DataContext = this;
            AccommodationReservationController = new AccommodationReservationController();
            CancelledReservationsNotificationsController = new CancelledReservationsNotificationsController();
            var reservations = AccommodationReservationController.GetAll().Where(r => !r.Canceled);
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(reservations);
        }

        private void Button_Click_Cancellation(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                if(DateTime.Today < SelectedReservation.StartDate.AddHours(-24))
                {
                    var reservation = AccommodationReservationController.FindAndCancel(SelectedReservation);
                    AccommodationReservationController.Update(reservation);
                    var message = $"Gost {SelectedReservation.Guest.Username} je otkazao rezervaciju {SelectedReservation.Accommodation.Name}({SelectedReservation.StartDate.ToString("dd.MM.yyyy.")} - {SelectedReservation.EndDate.ToString("dd.MM.yyyy.")})";
                    CancelledReservationsNotifications = new CancelledReservationsNotifications(message, false);
                    CancelledReservationsNotificationsController.Create(CancelledReservationsNotifications);
                    MessageBox.Show("Rezervacija je otkazana!");
                    Close();
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
    }
}
