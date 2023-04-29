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
    /// Interaction logic for AllToursPage.xaml
    /// </summary>
    public partial class AllToursPage : Page
    {
        private ToursViewModel _tours { get; set; }
        public AllToursPage()
        {
            InitializeComponent();
            _tours = new();
            this.DataContext = _tours;
        }

        private void DetailsBTN_Click(object sender, RoutedEventArgs e)
        {
            var window = new DetailedTourWindow(_tours.SelectedTour);
            window.Show();
        }
    }
}
