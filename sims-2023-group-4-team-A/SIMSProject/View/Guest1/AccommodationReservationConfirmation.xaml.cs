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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationConfirmation.xaml
    /// </summary>
    public partial class AccommodationReservationConfirmation : Window
    {
        public Accommodation Accommodation { get; set; } = new();
        List<AccommodationReservation> AccommodationReservations { get; set; } = new List<AccommodationReservation>();
        public AccommodationReservationConfirmation(Accommodation accommodation)
        {
            InitializeComponent();
            Accommodation = accommodation;
            NameTextBlock.Text = Accommodation.Name;
            int id = Accommodation.Id;

           
             

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

            if(Days >= Accommodation.MinReservationDays)
            {
                if (CheckReservations(AccommodationReservations, dateBegin, dateEnd))
                {
                    var confirm = new FreeAccommodationsSuggestions();
                    confirm.Show();
                }
                else
                {
                    //AccommodationReservation = new AccommodationReservation(...);
                }

            }
 
        }

        public bool CheckReservations(List<AccommodationReservation> reservations, DateTime startDate, DateTime endDate)
        {
            foreach (var reservation in reservations)
            {
                if (startDate >= reservation.StartDate && startDate <= reservation.EndDate)
                {
                    return true;  //Taken
                }
            }
            return false; //It's free
        }
    }
}
