using SIMSProject.Model;
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

namespace SIMSProject.View.Guest2
{
    /// <summary>
    /// Interaction logic for TourReservation.xaml
    /// </summary>
    public partial class TourReservation : Window
    {
        public Tour Tour { get; set; }
        public int GuestsForReservation;
        
        public TourReservation(Tour tour)
        {
            InitializeComponent();
            DataContext = this;
            this.Tour = tour;

            
        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            GuestsForReservation = Convert.ToInt32(ReservationNumber.Text);
            

        }
    }
}
