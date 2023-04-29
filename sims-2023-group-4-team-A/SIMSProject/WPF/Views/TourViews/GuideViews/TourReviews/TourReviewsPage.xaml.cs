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

        private void dgrGrades_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var window = new DetailedReviewWindow(ViewModel.AppointmentRating);
            window.ShowDialog();
        }
    }
}
