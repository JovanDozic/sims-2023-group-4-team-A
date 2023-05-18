using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.TourViewModels;
using SIMSProject.WPF.Views;
using SIMSProject.WPF.Views.TourViews.GuideViews;
using SIMSProject.WPF.Views.TourViews.GuideViews.CustomTourRequests;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;

namespace SIMSProject.View.GuideViews
{
    /// <summary>
    /// Interaction logic for GuideHomeWindow.xaml
    /// </summary>
    public partial class GuideHomeWindow : Window
    {
        private GuideHomeViewModel ViewModel { get; set; }
        public Guide Guide { get; set; } = new();
        public GuideHomeWindow(Guide guide)
        {
            InitializeComponent();
            this.Guide = guide;
            ViewModel = new(this.Guide);
            mainFrame.Content = new AllToursPage();
            this.DataContext = ViewModel;
        }

        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreation window = new();
            window.Show();
        }

        private void TodaysBTN_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new TodaysToursPage();
        }

        private void AllBTN_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new AllToursPage();
        }

        private void ReviewBTN_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new TourReviewsPage();   
        }

        private void Sign_outBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new SignInView();
            window.Show();
            this.Close();
        }

        private void StatisticsBTN_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new TourStatisticsPage();
        }

        private void RequestBTN_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new CustomRequestsPage();
        }

        private void RequestStatBTN_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Content = new CustomRequestsStatisticsPage();
        }
    }
}
