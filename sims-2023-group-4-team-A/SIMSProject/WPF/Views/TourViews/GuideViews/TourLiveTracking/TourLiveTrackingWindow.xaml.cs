using System.Windows;
using System.Windows.Input;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for TourLiveTrackingWindow.xaml
    /// </summary>
    public partial class TourLiveTrackingWindow : Window
    {
        private TourLiveTrackViewModel ViewModel;
        public TourLiveTrackingWindow(TourLiveTrackViewModel viewModel)
        {
            InitializeComponent();
            this.ViewModel = viewModel;
            ViewModel.RequestClose += (sender, args) => Close();
            this.DataContext = ViewModel;
        }
    }
}
