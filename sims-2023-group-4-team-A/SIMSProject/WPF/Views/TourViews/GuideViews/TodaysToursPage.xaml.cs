using SIMSProject.WPF.ViewModel.TourViewModels;
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
        private ToursViewModel ToursViewModel { get; set; } = new();
        public TodaysToursPage()
        {
            InitializeComponent();
            ToursViewModel.GetTodaysTours();
            this.DataContext = ToursViewModel;
        }
        private void CheckAppointmentsBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new LiveTrackAppointmentSelectionWindow(ToursViewModel.SelectedTour);
            window.Show();
        }
    }
}
