using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for LiveTrackAppointmentSelectionWindow.xaml
    /// </summary>
    public partial class LiveTrackAppointmentSelectionWindow : Window
    {
        //private TourAppointmentsViewModel ViewModel { get; set; }
        private AppointmentsViewModel _baViewModel { get; set; }
        public LiveTrackAppointmentSelectionWindow(Tour tour)
        {
            InitializeComponent();
            //ViewModel = new(tour);
            //this.DataContext = ViewModel;


            _baViewModel = new AppointmentsViewModel(tour);
            this.DataContext = _baViewModel;
        }

        private void StartTrackingBTN_Click(object sender, RoutedEventArgs e)
        {
            //ViewModel.StartIfActivated();
            //var window = new TourLiveTrackingWindow(ViewModel.SelectedAppointment, ViewModel.Tour.Tour);
            _baViewModel.StartIfActivated();
            LbAppointments.Items.Refresh();
            var window = new TourLiveTrackingWindow(_baViewModel.SelectedAppointment, _baViewModel.Tour.Tour, _baViewModel);
            window.Show();
        }

        private void LbAppointments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _baViewModel.Appointment.TourAppointment = _baViewModel.SelectedAppointment;
        }
    }
}
