using System.Windows;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourLiveTrackingWindow.xaml
    /// </summary>
    public partial class TourLiveTrackingWindow : Window
    {
        private TourLiveTrackViewModel ViewModel { get; set; }
        public TourLiveTrackingWindow(TourLiveTrackViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.AddGuests();
            this.DataContext = ViewModel;
        }

        private void NextBTN_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GoNext();
            LbGuests.Items.Refresh();
            CurrentKeyPointTB.Text = ViewModel.Appointment.CurrentKeyPoint.ToString();
            
        }

        private void PauseBTN_Click(object sender, RoutedEventArgs e)
        {
            CurrentKeyPointTB.Text = ViewModel.Appointment.CurrentKeyPoint.ToString();
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
            LbGuests.Items.Refresh();
        }

        private void LbGuests_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Sign_guestBTN.IsEnabled = ViewModel.SelectedGuest.GuestStatus == GuestAttendance.ABSENT;
        }
    }
}
