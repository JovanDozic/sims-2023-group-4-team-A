using SIMSProject.Domain.Models.AccommodationModels;
using SIMSProject.WPF.CustomControls;
using SIMSProject.WPF.ViewModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
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

namespace SIMSProject.WPF.Views.OwnerViews.OwnerAccommodationViews
{
    public partial class OwnerYearlyStatisticsView : Page, INavigationService
    {
        private AccommodationStatisticsViewModel _viewModel;

        public OwnerYearlyStatisticsView(Accommodation accommodation)
        {
            InitializeComponent();
            _viewModel = new(accommodation);
            DataContext = _viewModel;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        public void GoBack()
        {
            NavigationService?.GoBack();
        }
    }
}
