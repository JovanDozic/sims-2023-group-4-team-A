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
        private AcceptRequestViewModel ViewModel;
        public AcceptRequestWindow(AcceptRequestViewModel viewModel)
        {
            this.ViewModel = viewModel;
            InitializeComponent();
            this.DataContext = ViewModel;

            foreach(var date in ViewModel.BusyDates)
            {
                dpAppointment.BlackoutDates.Add(new CalendarDateRange(date));
            }
        }
    }
}
