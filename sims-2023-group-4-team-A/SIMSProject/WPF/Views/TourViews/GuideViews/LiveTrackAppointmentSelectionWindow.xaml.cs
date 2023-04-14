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
        //private TourAppointmentsViewModel _viewModel { get; set; }
        private AppointmentsViewModel _baViewModel { get; set; }
        public LiveTrackAppointmentSelectionWindow(Tour tour)
        {
            InitializeComponent();
            //_viewModel = new(tour);
            //this.DataContext = _viewModel;


            _baViewModel = new AppointmentsViewModel(tour);
            this.DataContext = _baViewModel;
        }

        private void StartTrackingBTN_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.StartIfActivated();
            //var window = new TourLiveTrackingWindow(_viewModel.SelectedAppointment, _viewModel.Tour.Tour);
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
