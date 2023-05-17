using SIMSProject.Application.DTOs;
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
using System.Windows.Shapes;

namespace SIMSProject.WPF.Views.TourViews.GuideViews
{
    /// <summary>
    /// Interaction logic for DetailedReviewWindow.xaml
    /// </summary>
    public partial class DetailedReviewWindow : Window
    {
        private AppointmentRatingViewModel ViewModel { get; set; } = new();
        public DetailedReviewWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }
    }
}
