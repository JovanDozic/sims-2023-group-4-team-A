using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModel.TourViewModels;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for LiveTrackAppointmentSelectionWindow.xaml
    /// </summary>
    public partial class LiveTrackAppointmentSelectionWindow : Window
    {
        private TourAppointmentsViewModel ViewModel { get; set; }
        public LiveTrackAppointmentSelectionWindow(Tour tour)
        {
            InitializeComponent();
            ViewModel = new(tour);
            this.DataContext = ViewModel;
        }

        private void StartTrackingBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.StartIfActivated();
            var window = new TourLiveTrackingWindow(ViewModel.SelectedAppointment, ViewModel.Tour.GetTour());
            window.Show();
        }
    }
}
