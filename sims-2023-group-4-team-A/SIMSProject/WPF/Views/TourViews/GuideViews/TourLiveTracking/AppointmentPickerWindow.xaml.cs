using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;
using System.Linq;
using System.Windows;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for AppointmentPickerWindow.xaml
    /// </summary>
    public partial class AppointmentPickerWindow : Window
    {
        private AppointmentPickerViewModel ViewModel { get; set; } = new();
        public AppointmentPickerWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void LbAppointments_GotFocus(object sender, RoutedEventArgs e)
        {
            LbAppointments.ToolTip = "Ctrl+S za praćenje uživo";
        }

        private void StartTrackingBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new TourLiveTrackingWindow();
            this.Close();
            window.Show();
        }
    }
}
