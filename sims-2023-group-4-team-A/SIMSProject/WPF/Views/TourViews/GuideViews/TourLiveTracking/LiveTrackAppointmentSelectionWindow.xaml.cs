using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for LiveTrackAppointmentSelectionWindow.xaml
    /// </summary>
    public partial class LiveTrackAppointmentSelectionWindow : Window
    {
        private TourLiveTrackViewModel ViewModel { get; set; }
        public LiveTrackAppointmentSelectionWindow(Tour tour)
        {
            InitializeComponent();

            ViewModel = new TourLiveTrackViewModel(tour);
            this.DataContext = ViewModel;
        }

        private void StartTrackingBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartIfActivated();
            LbAppointments.Items.Refresh();
            var window = new TourLiveTrackingWindow(ViewModel);
            window.Show();
        }

        private void LbAppointments_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ViewModel.Appointment.TourAppointment = ViewModel.SelectedAppointment;
        }
    }
}
