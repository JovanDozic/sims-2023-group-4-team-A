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

namespace SIMSProject.WPF.Views.Guest1.Pages
{
    /// <summary>
    /// Interaction logic for AccommodationReview.xaml
    /// </summary>
    public partial class AccommodationReview : Page
    {
        private AccommodationViewModel _accommodationViewModel;
        public AccommodationReview(AccommodationViewModel accommodationViewModel)
        {
            InitializeComponent();
            _accommodationViewModel = accommodationViewModel;
            DataContext = _accommodationViewModel;
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
