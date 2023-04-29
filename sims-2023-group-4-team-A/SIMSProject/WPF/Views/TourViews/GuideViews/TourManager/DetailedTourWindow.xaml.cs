using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;
using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System.Windows;
using System.Windows.Controls;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for DetailedTourView.xaml
    /// </summary>
    public partial class DetailedTourWindow : Window
    {
        private TourManagerViewModel ViewModel { get; set; } = new();
        public DetailedTourWindow(TourManagerViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            ViewModel.GetAllAppointments();
            this.DataContext = ViewModel;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CancelAppointment();
            LbAppointments.Items.Refresh();
            IsCancelEnabled();
        }


        private void IsCancelEnabled()
        {
            CancelBtn.IsEnabled = ViewModel.SelectedAppointment.TourStatus == Status.INACTIVE;
        }

        private void LbAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsCancelEnabled();
        }
    }
}
