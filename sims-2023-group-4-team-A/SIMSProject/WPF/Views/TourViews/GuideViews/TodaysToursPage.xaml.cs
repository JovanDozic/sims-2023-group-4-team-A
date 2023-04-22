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
    /// Interaction logic for TodaysToursPage.xaml
    /// </summary>
    public partial class TodaysToursPage : Page
    {
        private ToursViewModel ViewModel { get; set; }
        public TodaysToursPage()
        {
            InitializeComponent();
            ViewModel = new();
            ViewModel.GetTodaysTours();
            this.DataContext = ViewModel;
        }
        private void CheckAppointmentsBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new LiveTrackAppointmentSelectionWindow(ViewModel.SelectedTour);
            window.Show();
        }
    }
}
