using SIMSProject.WPF.ViewModels.TourViewModels;
using SIMSProject.WPF.ViewModels.TourViewModels.StatisticsViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for TourStatisticsPage.xaml
    /// </summary>
    public partial class TourStatisticsPage : Page
    {
        private TourStatisticsViewModel ViewModel { get; set; }
        public TourStatisticsPage()
        {
            InitializeComponent();
            Loaded += (sender, e) => MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            ViewModel = new TourStatisticsViewModel();
            this.DataContext = ViewModel;
        }
    }
}
