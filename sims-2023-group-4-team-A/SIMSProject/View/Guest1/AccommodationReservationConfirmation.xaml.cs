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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmation.xaml
    /// </summary>
    public partial class AccommodationReservationConfirmation : Window
    {
        public Guest User { get; set; } = new();
        public Accommodation Accommodation { get; set; } = new();
        List<AccommodationReservation> AccommodationReservations { get; set; } = new List<AccommodationReservation>();
        public AccommodationReservationController Controller { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; } = new();
        public AccommodationReservationConfirmation(Guest user, Accommodation accommodation)
        {
            InitializeComponent();
            Accommodation = accommodation;
            Controller = new AccommodationReservationController();
            NameTextBlock.Text = Accommodation.Name;
            User = user;
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Confirm_Button(object sender, RoutedEventArgs e)
        {
            DateTime dateBegin = (DateTime)DateBeginBox.SelectedDate;
            DateTime dateEnd = (DateTime)DateEndBox.SelectedDate;
            int Days = DaysBox.Value;
            TimeSpan duration = dateEnd - dateBegin;

            if(Days >= Accommodation.MinReservationDays && Days <= duration.Days)
            {
                if (CheckReservations(Controller.GetAll(), dateBegin, dateEnd))
                {
                    var confirm = new FreeAccommodationsSuggestions();
                    confirm.Show();
                }
                else
                {
                     
                   /* AccommodationReservation = new AccommodationReservation(Accommodation.Id, User.Id, dateBegin, dateEnd, Days);

                    Controller.Create(AccommodationReservation);
                    MessageBox.Show("Smestaj rezervisan!");

                    */
                }

            }
            else
            {
                MessageBox.Show("Broj dana nije prihvatljiv!");
            }
 
        }

        public bool CheckReservations(List<AccommodationReservation> reservations, DateTime startDate, DateTime endDate)
        {
            foreach (var reservation in reservations)
            {
                if ((startDate >= reservation.StartDate && startDate <= reservation.EndDate) ||
                    (endDate >= reservation.StartDate && endDate <= reservation.EndDate) ||
                    (startDate <= reservation.StartDate && endDate >= reservation.EndDate))
                {
                    return true;  //Taken
                }
            }
            return false; //It's free
        }
    }
}
