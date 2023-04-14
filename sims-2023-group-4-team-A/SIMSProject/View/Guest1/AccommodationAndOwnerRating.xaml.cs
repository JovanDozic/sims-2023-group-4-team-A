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
using System.Windows.Shapes;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
using SIMSProject.Controller;
using System.Collections.ObjectModel;
using SIMSProject.Controller.UserController;


namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationAndOwnerRating.xaml
    /// </summary>
    public partial class AccommodationAndOwnerRating : Window
    {
        public Guest User = new();
        public List<AccommodationReservation> AccommodationReservations { get; set; }
        public ObservableCollection<AccommodationReservation> Reservations { get; set; }
        public AccommodationReservationController AccommodationReservationController { get; set; }
        public Accommodation Accommodation { get; set; } = new();
        public AccommodationController AccommodationController { get; set; }
        public List<Accommodation> Accommodations { get; set; } = new();
        public Owner Owner { get; set; }
        public List<Owner> Owners { get; set; } = new();
        public Model.AccommodationAndOwnerRating Rating { get; set; } = new();

        public AccommodationAndOwnerRating(Guest user)
        {
            InitializeComponent();
            DataContext = this;
            User = user;
            Reservations = new ObservableCollection<AccommodationReservation>();
            AccommodationReservationController = new AccommodationReservationController();
            AccommodationController = new AccommodationController();
            AccommodationReservations = new List<AccommodationReservation>();
            Accommodations = new List<Accommodation>();
            Owners = new List<Owner>();
            AddReservationsToCombo();
           
        }

        public void AddReservationsToCombo()
        {
            foreach (var reservation in AccommodationReservationController.GetAll())
            {
                if (reservation.EndDate < DateTime.Today && !reservation.Canceled && reservation.Guest.Id == User.Id)
                {
                    Reservations.Add(reservation);
                }
            }
            ReservationsCombo.ItemsSource = Reservations;
        }

        private void ReservationsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReservationsCombo.SelectedItem != null)
            {
                AccommodationReservation selectedReservation = ReservationsCombo.SelectedItem as AccommodationReservation;
                selectedReservation.Accommodation.Owner = new OwnerController().GetByID(selectedReservation.Accommodation.Owner.Id);
                OwnerNameTextBlock.Text = selectedReservation.Accommodation.Owner.Username;
            }
            
        }

    }
}
