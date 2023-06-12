using SIMSProject.Domain.Models.TourModels;
using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for AppointmentPickerWindow.xaml
    /// </summary>
    public partial class AppointmentPickerWindow : Window
    {
        private AppointmentPickerViewModel ViewModel;
        public AppointmentPickerWindow(AppointmentPickerViewModel viewModel)
        {
            InitializeComponent();
            Loaded += (sender, e) =>
            MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            this.ViewModel = viewModel;
            this.DataContext = ViewModel;
            ViewModel.RequestOpen += (sender, args) => OpenLiveTrackView();
        }
        private void OpenLiveTrackView()
        {
            var window = new TourLiveTrackingWindow(ViewModel.NextViewModel);
            this.Close();
            window.Show();
        }
    }
}
