using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.TourViews.GuideViews.CustomTourRequests
{
    /// <summary>
    /// Interaction logic for AcceptRequestWindow.xaml
    /// </summary>
    public partial class AcceptRequestWindow : Window
    {
        private AcceptRequestViewModel ViewModel = new();
        public AcceptRequestWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;

            foreach(var date in ViewModel.BusyDates)
            {
                dpAppointment.BlackoutDates.Add(new CalendarDateRange(date));
            }
        }
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            var window = new TourCreation();
            window.Show();
        }
    }
}
