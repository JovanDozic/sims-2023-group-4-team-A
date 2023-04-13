using System.Windows;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModel.TourViewModels;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourLiveTrackingWindow.xaml
    /// </summary>
    public partial class TourLiveTrackingWindow : Window
    {
        private LiveTrackViewModel ViewModel;
        
        public TourLiveTrackingWindow(TourAppointment appointment, Tour tour)
        {
            InitializeComponent();
            ViewModel = new(tour, appointment);
            this.DataContext = ViewModel;
        }

        private void NextBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoNext();
            CurrentKeyPointTB.Text = ViewModel.Appointment.CurrentKeyPoint.ToString();
        }

        private void PauseBTN_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.EndAppointment();
            Close();
        }

        private void Sign_guestBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SignUpGuest();
        }
    }
}
