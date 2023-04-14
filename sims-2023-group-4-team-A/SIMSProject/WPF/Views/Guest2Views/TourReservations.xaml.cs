using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Model;
using SIMSProject.Model.UserModel;
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

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourReservations.xaml
    /// </summary>
    public partial class TourReservations : Window
    {
        public Guest User = new();
        public TourAppointment SelectedTourAppointment { get; set; } = new TourAppointment();
        public ObservableCollection<TourAppointment> TourAppointments { get; set; }
        public TourReservations(Guest user)
        {
            InitializeComponent();
            this.DataContext = this;
            User =user;

        }

        private void RateGuide_Click(object sender, RoutedEventArgs e)
        {
            RateGuide rateGuide = new RateGuide(User, SelectedTourAppointment);
            rateGuide.Show();
        }
    }
}
