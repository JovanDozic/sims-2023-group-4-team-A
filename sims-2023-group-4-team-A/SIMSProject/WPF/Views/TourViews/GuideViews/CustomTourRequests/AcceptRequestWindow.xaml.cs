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
        private AcceptRequestViewModel ViewModel;
        public AcceptRequestWindow(AcceptRequestViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
            foreach(var date in ViewModel.BusyDates)
            {
                dpAppointment.BlackoutDates.Add(new CalendarDateRange(date));
            }

            ViewModel.RequestOpen += (sender, args) => OpenCreationView();
            this.DataContext = ViewModel;
        }
        private void OpenCreationView()
        {
            var window = new TourCreation(ViewModel.NextViewModel);
            window.Show();
            this.Close();
        }
    }
}
