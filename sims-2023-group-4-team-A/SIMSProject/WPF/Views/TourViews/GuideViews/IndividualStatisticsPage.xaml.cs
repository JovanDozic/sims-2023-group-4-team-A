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
    /// Interaction logic for IndividualStatisticsPage.xaml
    /// </summary>
    public partial class IndividualStatisticsPage : Page
    {

        private ToursViewModel ViewModel { get; set; }

        public IndividualStatisticsPage()
        {
            InitializeComponent();
            ViewModel = new ToursViewModel();
            ViewModel.GetFinishedTours();
            this.DataContext = ViewModel;
        }
    }
}
