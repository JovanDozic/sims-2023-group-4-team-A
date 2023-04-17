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
        private AppointmentRatingViewModel viewModel { get; set; }
        public DetailedReviewWindow(TourAppontmentRatingDTO rating)
        {
            InitializeComponent();
            viewModel = new(rating);
            this.DataContext = viewModel;

            AddImages();
        }


        private void AddImages()
        {
            cstmImages.AddImages(viewModel.Rating.Rating.ImageURLs);
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ReportReview();
        }
    }
}
