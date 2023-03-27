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

namespace SIMSProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for AccommodationReservationList.xaml
    /// </summary>
    public partial class AccommodationReservationList : Window
    {
        public AccommodationReservation AccommodationReservation { get; set; }
        public ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        private AccommodationReservationController AccommodationReservationController { get; set; }
        public AccommodationReservationList()
        {
            InitializeComponent();
            DataContext = this;
            AccommodationReservationController = new AccommodationReservationController();
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(AccommodationReservationController.GetAll());
        }

       
    }
}
