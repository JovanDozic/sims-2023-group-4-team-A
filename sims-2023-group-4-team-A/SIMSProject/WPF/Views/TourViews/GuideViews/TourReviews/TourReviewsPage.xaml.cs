using SIMSProject.WPF.ViewModels.TourViewModels.ReviewsViewModels;
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
    /// Interaction logic for TourReviewsPage.xaml
    /// </summary>
    public partial class TourReviewsPage : Page
    {
        private TourRatingsViewModel ViewModel { get; set; } = new();
        public TourReviewsPage()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }
        private void tbSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = "Pretraži po nazivu ture";
        }
        private void tbSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            tbSearch.Text = string.Empty;
        }
        private void btnOpenDetails_Click(object sender, RoutedEventArgs e)
        {
            var window = new DetailedReviewWindow();
            window.Show();
        }
    }
}
