using SIMSProject.Application.DTOs;
using SIMSProject.WPF.ViewModels.TourViewModels;
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
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for DetailedReviewWindow.xaml
    /// </summary>
    public partial class DetailedReviewWindow : Window
    {
        private AppointmentRatingViewModel ViewModel { get; set; }
        public DetailedReviewWindow(TourAppontmentRatingDTO rating)
        {
            InitializeComponent();
            ViewModel = new(rating);
            this.DataContext = ViewModel;

            btnIsEnabled();
            AddImages();
        }

        private void AddImages()
        {
            cstmImages.AddImages(ViewModel.Rating.Rating.ImageURLs);
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ReportReview();
            btnIsEnabled();
        }
        private void btnIsEnabled()
        {
            btnReport.IsEnabled = !ViewModel.Rating.Rating.Reported;

            switch (btnReport.IsEnabled)
            {
                case true: btnReport.Content = "Prijavi\nCtrl+P"; break;
                case false: btnReport.Content = "Recenzija\nprijavljena"; break;
            }
        }
    }
}
