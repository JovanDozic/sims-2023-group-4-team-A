using SIMSProject.Domain.Models.TourModels;
using SIMSProject.WPF.ViewModels.TourViewModels.LiveTrackingViewModels;
using SIMSProject.WPF.ViewModels.TourViewModels.ManagerViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for DetailedTourView.xaml
    /// </summary>
    public partial class DetailedTourWindow : Window
    {
        private DetailedTourViewModel ViewModel { get; set; } = new();
        public DetailedTourWindow(DetailedTourViewModel viewModel)
        {
            InitializeComponent();
            PreviewKeyDown += (s, e) => { if (e.Key == Key.Escape) this.Close(); };
            this.ViewModel = viewModel;
            this.DataContext = ViewModel;
        }
    }
}
