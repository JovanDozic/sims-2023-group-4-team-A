using SIMSProject.View.GuideViews;
using SIMSProject.WPF.ViewModels.TourViewModels.CustomTourRequestsViewModels;
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

namespace SIMSProject.WPF.Views.TourViews.GuideViews.CustomTourRequests
{
    /// <summary>
    /// Interaction logic for CustomRequestsStatisticsPage.xaml
    /// </summary>
    public partial class CustomRequestsStatisticsPage : Page
    {
        private CustomTourRequestsStatisticsViewModel ViewModel { get; set; } = new();
        public CustomRequestsStatisticsPage()
        {
            InitializeComponent();
            ViewModel.RequestOpen += (sender, args) => OpenCreationView();
            this.DataContext = ViewModel;
        }
        private void OpenCreationView()
        {
            var window = new TourCreation(ViewModel.NextViewModel);
            window.Show();
        }
        private void rdbAll_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (rdbLocation.IsChecked == true)
            {
                rdbAll.ToolTip = "Pritisnite L za prikaz statistike po lokacijama";
            }
            if (rdbLanguage.IsChecked == true)
            {
                rdbAll.ToolTip = "Pritisnite J za prikaz statistike po jezicima";
            }

        }
        private void rdbYear_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (rdbLocation.IsChecked == true)
            {
                rdbAll.ToolTip = "Pritisnite A za prikaz godišnje statistike po lokacijama";
            }
            if (rdbLanguage.IsChecked == true)
            {
                rdbAll.ToolTip = "Pritisnite B za prikaz godišnje statistike po jezicima";
            }
        }

        private void rdbLocation_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            rdbLocation.ToolTip = "Izaberite lokaciju";
        }

        private void rdbLanguage_GotKeyboardFocus(object sender, RoutedEventArgs e)
        {
            rdbLanguage.ToolTip = "Izaberite jezik";
        }
    }
}
