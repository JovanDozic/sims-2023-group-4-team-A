using SIMSProject.WPF.ViewModels.TourViewModels;
using SIMSProject.WPF.ViewModels.TourViewModels.StatisticsViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for IndividualStatisticsPage.xaml
    /// </summary>
    public partial class IndividualStatisticsPage : Page
    {
        private TourStatisticsViewModel ViewModel { get; set; }
        public IndividualStatisticsPage()
        {
            InitializeComponent();
            ViewModel = new TourStatisticsViewModel();
            ViewModel.GetFinishedTours();
            this.DataContext = ViewModel;
        }
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new TourStatisticsPage());
        }
    }
}
