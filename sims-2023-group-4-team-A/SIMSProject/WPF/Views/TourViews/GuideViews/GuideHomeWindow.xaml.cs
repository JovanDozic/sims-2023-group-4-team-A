using SIMSProject.Application.Services;
using SIMSProject.Domain.Injectors;
using SIMSProject.Domain.Models;
using SIMSProject.Domain.Models.TourModels;
using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.Views.TourViews.GuideViews;
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

        public Guide Guide { get; set; } = new();
        public GuideHomeWindow(Guide guide)
        {
            InitializeComponent();
            this.Guide = guide;
            frame.Content = new AllToursPage();

        }

        private void CreateTour_Click(object sender, RoutedEventArgs e)
        {
            TourCreation window = new(Guide);
            window.Show();
        }

        private void TodaysBTN_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new TodaysToursPage();
        }

        private void AllBTN_Click(object sender, RoutedEventArgs e)
        {
            frame.Content = new AllToursPage();
        }

        private void ReviewBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new DetailedReviewWindow();
            window.Show();
        }
    }
}
