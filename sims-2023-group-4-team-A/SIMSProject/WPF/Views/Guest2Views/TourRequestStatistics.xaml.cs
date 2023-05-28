using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.Guest2ViewModels;
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

namespace SIMSProject.WPF.Views.Guest2Views
{
    /// <summary>
    /// Interaction logic for TourRequestStatistics.xaml
    /// </summary>
    public partial class TourRequestStatistics : Page
    {
        private Guest2 _user = new();
        private TourRequestStatisticsViewModel _viewModel;
        
        public TourRequestStatistics(Guest2 user)
        {
            InitializeComponent();
            _user = user;
            _viewModel = new TourRequestStatisticsViewModel(_user);
            this.DataContext = _viewModel;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
