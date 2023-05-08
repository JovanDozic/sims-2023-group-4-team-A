using SIMSProject.Domain.Models.UserModels;
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
    /// Interaction logic for AccommodationList.xaml
    /// </summary>
    public partial class AccommodationList : Page
    {
        private readonly User _user = new();
        private readonly AccommodationViewModel _accommodationViewModel;
        public AccommodationList(User user)
        {
            InitializeComponent();
            _user = user;
            _accommodationViewModel = new(_user);
            DataContext = _accommodationViewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonShow.IsEnabled = !_accommodationViewModel.IsNotSelected();
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AccommodationReview(_accommodationViewModel, _user));
        }
    }
}
