using SIMSProject.Domain.Models.UserModels;
using SIMSProject.WPF.ViewModels.AccommodationViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

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
