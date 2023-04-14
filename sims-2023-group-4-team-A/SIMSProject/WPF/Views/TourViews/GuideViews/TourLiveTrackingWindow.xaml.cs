using System.Windows;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourLiveTrackingWindow.xaml
    /// </summary>
    public partial class TourLiveTrackingWindow : Window
    {
        //private LiveTrackViewModel _viewModel { get; set; }
        private AppointmentsViewModel _viewModel { get; set; }
        
        public TourLiveTrackingWindow(TourAppointment appointment, Tour tour, AppointmentsViewModel viewModel)
        {
            InitializeComponent();
            //_viewModel = new(tour, appointment);
            //this.DataContext = _viewModel;
            _viewModel = viewModel;
            _viewModel.AddGuests();
            this.DataContext = _viewModel;
        }

        private void NextBTN_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.GoNext();
            //CurrentKeyPointTB.Text = _viewModel.Appointment.CurrentKeyPoint.ToString();
            _viewModel.GoNext();
            LbGuests.Items.Refresh();
            CurrentKeyPointTB.Text = _viewModel.Appointment.CurrentKeyPoint.ToString();
            
        }

        private void PauseBTN_Click(object sender, RoutedEventArgs e)
        {
            //CurrentKeyPointTB.Text = _viewModel.Appointment.CurrentKeyPoint.ToString();
            CurrentKeyPointTB.Text = _viewModel.Appointment.CurrentKeyPoint.ToString();
            Close();
        }

        private void CloseBTN_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.EndAppointment();
            _viewModel.EndAppointment();
            Close();
        }

        private void Sign_guestBTN_Click(object sender, RoutedEventArgs e)
        {
            //_viewModel.SignUpGuest();
            _viewModel.SignUpGuest();
            LbGuests.Items.Refresh();
        }
    }
}
