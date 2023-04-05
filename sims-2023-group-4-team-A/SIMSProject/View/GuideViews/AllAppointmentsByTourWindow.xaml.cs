using SIMSProject.Controller;
using SIMSProject.Domain.Models.TourModels;
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

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for AllAppointmentsByTourWindow.xaml
    /// </summary>
    public partial class AllAppointmentsByTourWindow : Window
    {
        private readonly VoucherController _voucherController = new();

        public ObservableCollection<TourAppointment> AllAppointments {  get; set; }
        public TourAppointment SelectedAppointment { get; set; } = new();
        public AllAppointmentsByTourWindow(int tourId)
        {
            InitializeComponent();
            this.DataContext = this;

            AllAppointments = new(GuideInitialWindow.tourAppointmentController.GetAllByTourId(tourId));
        }

        private void Cancel_TourBTN_Click(object sender, RoutedEventArgs e)
        {
            if (!GuideInitialWindow.tourAppointmentController.CancelAppointment(SelectedAppointment))
            {
                MessageBox.Show("Greška! Možete otkazati termin najkasnije 48 sati pred početak!");
                return;
            }
            List<TourGuest> guests = GuideInitialWindow.tourGuestController.GetGuestsIds(SelectedAppointment.Id);
            _voucherController.GiveVouchers(guests, "Termin otkazan");
            MessageBox.Show("Uspešno ste otkazali termin.");

        }
    }
}
